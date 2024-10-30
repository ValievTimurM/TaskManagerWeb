using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.Runtime.InteropServices;
using TaskManager.Application.Interfaces.Services.Ref;
using TaskManager.Application.Models.Auth;
using TaskManager.Application.Models.ViewModels;
using TaskManagerWeb.Client.AuthProvider;

namespace TaskManagerWeb.Client.Components
{
  public partial class AddCommnetModal
  {
    [Inject]
    private NotificationService _notificationService { get; set; }
    [Inject]
    public DialogService _dialogService { get; set; }
    [Inject]
    public IClientTaskService _taskService { get; set; }
    [CascadingParameter]
    Task<AuthenticationState> _authenticationState { get; set; }
    [Parameter]
    public Guid TaskId { get; set; }
    [Parameter]
    public bool FirstAdd { get; set; } = false;
		[Parameter]
		public TaskViewModel? TaskModel { get; set; } = null;


		private CommentViewModel _commentViewModel;
    private bool _pageLoading;
    private string _currenUser;

		protected override async Task OnInitializedAsync()
		{
			try
			{
				_pageLoading = true;
				_currenUser = (await _authenticationState).User?.Identity?.Name ?? "";
				_commentViewModel = CommentViewModel.New(TaskId, _currenUser);
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

		private async Task CreateComment()
    {
      try
      {
        if (FirstAdd)
        {
          TaskModel?.Comments.Add(_commentViewModel);
				}
        else
        {
					await _taskService.CreateComment(_commentViewModel);
				}
        _notificationService.Notify(new NotificationMessage
        {
          Severity = NotificationSeverity.Success,
          Summary = "Комментарий добавлен",
          Duration = 4000
        });
        _dialogService.Close();
      }
      catch (Exception e)
      {
        _notificationService.Notify(new NotificationMessage
        {
          Severity = NotificationSeverity.Warning,
          Summary = "Ошибка добавления комментария",
          Detail = e.Message,
          Duration = 4000
        });
      }
    }

    private void Close()
    {
      _dialogService.Close();
    }

  }
}
