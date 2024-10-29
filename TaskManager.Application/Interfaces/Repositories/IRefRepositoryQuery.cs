using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Application.Interfaces.Repositories
{
  public interface IRefRepositoryQuery
  {
    Task<IList<TaskModel>> FetchAllTasks();
    Task<TaskModel> FetchTaskBy(Guid id);
  }
}
