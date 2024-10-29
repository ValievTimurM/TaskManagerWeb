using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Models.Common;
using TaskManager.Application.Models.ViewModels;
using TaskManager.Core.Models;

namespace TaskManager.Application.Interfaces.Services.Ref
{
  public interface ITaskService
  {
    Task<IList<TaskViewModel>> Get();
    Task<TaskViewModel> GetBy(Guid id);
    Task<ServiceResult> Add(TaskViewModel item);
    Task<ServiceResult> Update(TaskViewModel item);
    Task<ServiceResult> Delete(Guid id);
  }
}
