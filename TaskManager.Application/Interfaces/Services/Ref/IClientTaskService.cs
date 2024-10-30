using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Models.ViewModels;

namespace TaskManager.Application.Interfaces.Services.Ref
{
  public interface IClientTaskService
  {
    Task CreateTask(TaskViewModel item);
    Task UpdateTask(TaskViewModel item);
    Task DeleteTask(TaskViewModel item);
    Task<IList<TaskViewModel>> GetTasks();
    Task<TaskViewModel> GetTaskBy(Guid id);
    Task CreateComment(CommentViewModel item);
  }
}
