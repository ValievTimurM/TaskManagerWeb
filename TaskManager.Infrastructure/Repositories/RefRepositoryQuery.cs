using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces.Repositories;
using TaskManager.Core.Models;
using TaskManager.Infrastructure.DB;

namespace TaskManager.Infrastructure.Repositories
{
  public class RefRepositoryQuery : IRefRepositoryQuery
  {
    private readonly TMContext _dbContext;
    public RefRepositoryQuery(TMContext context)
       => _dbContext = context;

    public async Task<IList<TaskModel>> FetchAllTasks()
    {
      return await _dbContext.Tasks.Include(p=>p.Comments).ToListAsync();
    }
    public async Task<TaskModel> FetchTaskBy(Guid id)
    {
      return await _dbContext.Tasks.Include(p => p.Comments).FirstOrDefaultAsync(p=>p.Id == id);
    }
  }
}
