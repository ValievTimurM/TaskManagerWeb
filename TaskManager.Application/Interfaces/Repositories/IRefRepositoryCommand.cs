using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Application.Interfaces.Repositories
{
  public interface IRefRepositoryCommand
  {
    Task AddTask(TaskModel item);
    Task UpdateTask(TaskModel item);
    Task DeleteTask(Guid id);
  }
}
