using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces.Repositories;
using TaskManager.Core.Models;
using TaskManager.Infrastructure.DB;

namespace TaskManager.Infrastructure.Repositories
{
  public class RefRepositoryCommand : IRefRepositoryCommand
  {
    private readonly TMContext _dbContext;
    public RefRepositoryCommand(TMContext context)
       => _dbContext = context;

    #region TaskModel
    public async Task AddTask(TaskModel item)
    {
      _dbContext.Tasks.Add(item);
      await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTask(TaskModel item)
    {
      var itemOld = await _dbContext.Tasks.FirstOrDefaultAsync(x=>x.Id == item.Id);
      if (itemOld != null)
      {
        itemOld.SyncProperties(item);
        await _dbContext.SaveChangesAsync();
      }
    }

    public async Task DeleteTask(Guid id)
    {
      var itemOld = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
      if (itemOld != null)
      {
        _dbContext.Tasks.Remove(itemOld);
        await _dbContext.SaveChangesAsync();
      }
    }

    #endregion
  }
}
