using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using TaskManager.Application.Interfaces.Services.Auth;
using TaskManager.Application.Interfaces.Services.Ref;
using TaskManager.Application.Models.ViewModels;
using TaskManagerWeb.Client.AuthProvider;
using TaskManagerWeb.Client.Components;

namespace TaskManagerWeb.Client.Pages
{
	public partial class Index
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

		private IList<TaskViewModel> _viewModels;
		private RadzenDataGrid<TaskViewModel> _modelsGrid;
		private string? _currenUser;
		private bool _pageLoading;
		private bool _loading;


		protected override async Task OnInitializedAsync()
		{
			try
			{
				_pageLoading = true;
				await Load();
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

		private async Task Load()
		{
      _currenUser = (await _authenticationState).User?.Identity?.Name ?? string.Empty;
			if(_currenUser != string.Empty)
				_viewModels = await _taskService.GetTasks();
		}

		private async Task Add()
		{
			_navigationManager.NavigateTo("task", false);
		}

		private async Task Update(TaskViewModel item)
		{
			_navigationManager.NavigateTo($"task/{item.Id}", false);
		}

		private async Task Delete(TaskViewModel item)
		{
			try
			{
				var confirm = await ShowConfirmDialog();
				if (confirm is null || (bool)confirm == false)
					return;

				await _taskService.DeleteTask(item);
				await Reload();

			}
			catch (Exception e)
			{
				_pageLoading = false;

				_notificationService.Notify(new NotificationMessage
				{
					Severity = NotificationSeverity.Warning,
					Summary = "Ошибка удаления задачи",
					Detail = e.Message,
					Duration = 4000
				});
			}
		}

		private async Task<bool?> ShowConfirmDialog()
		{
			var result = await _radzenDialog.Confirm("Это действие невозможно отменить, продолжить удаление?",
																							 "Уведомление системы",
																							 new ConfirmOptions() { OkButtonText = "Да", CancelButtonText = "Нет" });
			return result;
		}

		private async Task AddComment(TaskViewModel item)
		{
			await ShowAddCommentDialog(item.Id);
			StateHasChanged();
		}

		private async Task ShowAddCommentDialog(Guid taskId)
		{
			var result = await _radzenDialog.OpenAsync<AddCommnetModal>("Дабавление комментария",
																							 new Dictionary<string, object>() { { "TaskId", taskId } , { "FirstAdd", false } },
																							 new DialogOptions() { Width = "500px", Height = "auto", Resizable = false, Draggable = false });
			
		}

		private async Task Reload()
		{
			await Load();
			if (_modelsGrid is not null)
			{
				await InvokeAsync(_modelsGrid.Reload);
			}
			StateHasChanged();
		}

	}
}
