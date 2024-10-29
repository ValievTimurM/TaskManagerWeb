using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces.Repositories;
using TaskManager.Application.Interfaces.Services.Ref;
using TaskManager.Application.Mapping;
using TaskManager.Application.Models.Common;
using TaskManager.Application.Models.ViewModels;
using TaskManager.Core.Models;

namespace TaskManager.Application.Services.Ref
{
	public class TaskService : ITaskService
	{
		private readonly IRefRepositoryCommand _repositoryCommand;
		private readonly IRefRepositoryQuery _repositoryQuery;

		public TaskService(IRefRepositoryCommand repositoryCommand, IRefRepositoryQuery repositoryQuery)
		{
			_repositoryCommand = repositoryCommand;
			_repositoryQuery = repositoryQuery;
		}

		public async Task<IList<TaskViewModel>> Get()
		{
			var models = await _repositoryQuery.FetchAllTasks();

			var viewModels = models.Select(p => p.ToViewModel()).ToList();

			foreach (var item in viewModels)
			{
				item.Comments = models.FirstOrDefault(p=>p.Id==item.Id)?.Comments?.Select(p=>p.ToViewModel()).ToList();
			}

			return viewModels;
		}


		public async Task<TaskViewModel> GetBy(Guid id)
		{
			var model = await _repositoryQuery.FetchTaskBy(id);
			var viewModel = model.ToViewModel();
			viewModel.Comments = model.Comments.Select(p=>p.ToViewModel()).ToList();
			return viewModel;
		}

		public async Task<ServiceResult> Add(TaskViewModel item)
		{
			try
			{
				var entity = item.ToAddModel();
				entity.Comments = item.Comments.Select(p => p.ToModel()).ToList();
				await _repositoryCommand.AddTask(entity);
				return ServiceResult.Ok();
			}
			catch (Exception e)
			{
				return ServiceResult.Fail(e);
			}
		}


		public async Task<ServiceResult> Update(TaskViewModel item)
		{
			try
			{
				var entity = item.ToUpdateModel();
				entity.Comments = item.Comments.Select(p => p.ToModel()).ToList();
				await _repositoryCommand.UpdateTask(entity);
				return ServiceResult.Ok();
			}
			catch (Exception e)
			{
				return ServiceResult.Fail(e);
			}
		}

		public async Task<ServiceResult> Delete(Guid id)
		{
			try
			{
				await _repositoryCommand.DeleteTask(id);
				return ServiceResult.Ok();
			}
			catch (Exception e)
			{
				return ServiceResult.Fail(e);
			}
		}
	}
}
