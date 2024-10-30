using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Models.Common;
using TaskManager.Application.Models.ViewModels;

namespace TaskManager.Application.Interfaces.Services.Ref
{
  public interface ICommentService
  {
    Task<ServiceResult> Add(CommentViewModel item);
  }
}
