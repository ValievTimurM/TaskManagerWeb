using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Application.Models.ViewModels
{
	public class CommentViewModel
	{
		public CommentViewModel() { }
		public Guid Id { get; set; }
		public Guid TaskId { get; set; }
		[Required(ErrorMessage = "Это поле не может быть пустым!")]
		[StringLength(250, ErrorMessage = "Количество символов не может превышать 250!", MinimumLength = 2)]
		public string Text { get; set; }
		public string Creator { get; set; }
		public DateTime DateAdd { get; set; }

		public static CommentViewModel New(Guid id, Guid taskId, string creator, DateTime dateAdd, string text)
			=> new CommentViewModel()
			{
				Id = id,
				TaskId = taskId,
				Text = text,
				Creator = creator,
				DateAdd = dateAdd
			};
		public static CommentViewModel New(Guid id, string creator)
			=> new CommentViewModel()
			{
				Id = Guid.NewGuid(),
				TaskId = id,	
				Creator = creator,
				DateAdd = DateTime.Now
			};
	}
}
