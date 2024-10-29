using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models.Common;

namespace TaskManager.Core.Models
{
  public class Comment : EntityBase
  {
    private Comment(Guid id) : base(id) { }
    public Guid TaskId { get; set; }
    public TaskModel Task { get; set; }
    public string Text { get; set; }
    public string Creator { get; set; }
    public DateTime DateAdd { get; set; }

    public static Comment New(Guid taskId, string creator)
      => new Comment(Guid.NewGuid()) { 
        TaskId = taskId,
        Creator = creator,
        DateAdd = DateTime.Now
      };

		public static Comment New(Guid id, Guid taskId, string creator, DateTime date, string text)
			=> new Comment(id)
			{
				TaskId = taskId,
				Creator = creator,
				DateAdd = date,
        Text = text
			};
	}
}
