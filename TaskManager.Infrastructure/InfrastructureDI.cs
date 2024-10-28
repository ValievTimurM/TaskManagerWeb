using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Infrastructure.Auth;
using TaskManager.Infrastructure.DB;

namespace TaskManager.Infrastructure
{
  public static class InfrastructureDI
  {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<TMContext>(options => options.UseSqlite(configuration.GetConnectionString("DBCon")));
      services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<TMContext>();
      services.ConfigureApplicationCookie(options =>
      {
        options.Cookie.HttpOnly = false;
        options.Events.OnRedirectToLogin = context =>
        {
          context.Response.StatusCode = 401;
          return Task.CompletedTask;
        };
      });

      return services;
    }
  }
}
