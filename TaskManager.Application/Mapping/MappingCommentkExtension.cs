using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Models.ViewModels;
using TaskManager.Core.Models;

namespace TaskManager.Application.Mapping
{
	public static class MappingCommentExtension
	{
		public static Comment ToModel(this CommentViewModel model)
			=> Comment.New(id: model.Id,
										 text: model.Text,
										 taskId: model.TaskId,
										 creator: model.Creator,
										 date: model.DateAdd);

		public static CommentViewModel ToViewModel(this Comment model)
			=> CommentViewModel.New(id: model.Id,
															text: model.Text,
															taskId: model.TaskId,
															creator: model.Creator,
															dateAdd: model.DateAdd);
	}
}
