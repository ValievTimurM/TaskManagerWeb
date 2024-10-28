using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Infrastructure.Auth;

namespace TaskManager.Infrastructure.DB
{
  public class TMContext : IdentityDbContext<AppUser>
  {
    public TMContext(DbContextOptions<TMContext> options) : base(options)
    {

    }
    //public DbSet<Developer> Developers { get; set; }
  }
}
