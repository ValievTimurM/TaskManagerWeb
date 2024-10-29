using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;
using TaskManager.Application.Interfaces.Services.Ref;
using TaskManager.Application.Models.ViewModels;
using TaskManagerWeb.Client.AuthProvider;
using TaskManager.Core.Models.Enum;

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
    [CascadingParameter]
    Task<AuthenticationState> _authenticationState { get; set; }
    [Parameter]
    public string? Id { get; set; }

    private IList<StatusKind> _statuses = new List<StatusKind>() { StatusKind.Created, StatusKind.InProcess, StatusKind.Finished };
    private TaskViewModel _model;
    //private RadzenDataGrid<TaskViewModel> _modelsGrid;
    private string? _currenUser;
    private string _header;
    private string _dateTimeFormat = "dd.MM.yyyy HH:mm";
    private bool _pageLoading;
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
      }
      else
      {
        SetAddOption();
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

    private async void Back()
    {
      _navigationManager.NavigateTo("");
    }
  }
}
