using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;
using TaskManager.Application.Interfaces.Services.Ref;
using TaskManager.Application.Models.ViewModels;
using TaskManagerWeb.Client.AuthProvider;
using TaskManager.Core.Models.Enum;
using TaskManagerWeb.Client.Components;

namespace TaskManagerWeb.Client.Pages.Ref
{
  public partial class TaskPage
  {
    [Inject]
    private CustomStateProvider _authProvider { get; set; }

    [Inject]
    private IClientTaskService _taskService { get; set; }
    [Inject]
    private NotificationService _notificationService { get; set; }
    [Inject]
    private NavigationManager _navigationManager { get; set; }
		[Inject]
		private DialogService _radzenDialog { get; set; }
		[CascadingParameter]
    Task<AuthenticationState> _authenticationState { get; set; }
    [Parameter]
    public string? Id { get; set; }

    private IList<StatusKind> _statuses = new List<StatusKind>() { StatusKind.Created, StatusKind.InProcess, StatusKind.Finished };
    private TaskViewModel _model;
    private RadzenDataGrid<CommentViewModel> _modelsGrid;
    private string? _currenUser;
    private string _header;
    private string _dateTimeFormat = "dd.MM.yyyy HH:mm";
    private bool _pageLoading;
    private bool _firstAdd;
    private bool _isModelExist = true;
    private bool _loading;

    protected override async Task OnInitializedAsync()
    {
      try
      {
        _pageLoading = true;
        _currenUser = (await _authenticationState).User?.Identity?.Name;
        await SetPageOption();
        _pageLoading = false;
      }
      catch (Exception e)
      {
        _pageLoading = false;

        _notificationService.Notify(new NotificationMessage
        {
          Severity = NotificationSeverity.Warning,
          Summary = "Ошибка получения данных",
          Detail = e.Message,
          Duration = 4000
        });
      }
    }

    private async Task SetPageOption()
    {
      if(Id is not null)
      {
        await SetUpdateOption();
        _firstAdd = false;
      }
      else
      {
        SetAddOption();
        _firstAdd = true;
      }
      if(_model == null)
      {
        _isModelExist = false;
      }
    }

    private void SetAddOption()
    {
      _model = TaskViewModel.New(_currenUser);
      _header = $"Добавление задачи";
    }

    private async Task SetUpdateOption()
    {
      var result = Guid.TryParse(Id, out Guid id);

      if (result == false)
      {
        _isModelExist = false;
        _pageLoading = false;
        return;
      }

      await Load(id);
      _header = $"Изменение задачи {_model?.Name}";
    }

    private async Task Load(Guid id)
    {
      _model = await _taskService.GetTaskBy(id);
    }

    private async Task Submit()
    {
      try
      {
        if (Id is not null)
        {
          await _taskService.UpdateTask(_model);
        }
        else
        {
          await _taskService.CreateTask(_model);
        }
        Back();
      }
      catch (Exception e)
      {
        _pageLoading = false;

        _notificationService.Notify(new NotificationMessage
        {
          Severity = NotificationSeverity.Warning,
          Summary = "Ошибка получения данных",
          Detail = e.Message,
          Duration = 4000
        });
      }

    }

    private async Task AddComments()
    {
      await ShowAddCommentDialog(_model, _firstAdd);
		}

		private async Task ShowAddCommentDialog(TaskViewModel item, bool first)
		{
			var result = await _radzenDialog.OpenAsync<AddCommnetModal>("Добавление комментария",
																							 new Dictionary<string, object>() { { "TaskId", item.Id }, { "FirstAdd", first }, { "TaskModel", item } },
																							 new DialogOptions() { Width = "500px", Height = "auto", Resizable = false, Draggable = false });

      await Reload();

    }

    private async Task Reload()
    {
      if(_firstAdd == false) await Load(_model.Id);

      if (_modelsGrid is not null)
      {
        await InvokeAsync(_modelsGrid.Reload);
      }
      StateHasChanged();
    }

    private async void Back()
    {
      _navigationManager.NavigateTo("");
    }
  }
}
