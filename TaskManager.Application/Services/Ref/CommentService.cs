using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces.Repositories;
using TaskManager.Application.Interfaces.Services.Ref;
using TaskManager.Application.Mapping;
using TaskManager.Application.Models.Common;
using TaskManager.Application.Models.ViewModels;

namespace TaskManager.Application.Services.Ref
{
  public class CommentService : ICommentService
  {
    private readonly IRefRepositoryCommand _repositoryCommand;
    private readonly IRefRepositoryQuery _repositoryQuery;

    public CommentService(IRefRepositoryCommand repositoryCommand, IRefRepositoryQuery repositoryQuery)
    {
      _repositoryCommand = repositoryCommand;
      _repositoryQuery = repositoryQuery;
    }


    public async Task<ServiceResult> Add(CommentViewModel item)
    {
      try
      {
        var entity = item.ToModel();

        await _repositoryCommand.AddComment(entity);
        return ServiceResult.Ok();
      }
      catch (Exception e)
      {
        return ServiceResult.Fail(e);
      }
    }
  }
}

