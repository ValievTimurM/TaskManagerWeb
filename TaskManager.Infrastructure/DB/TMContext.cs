using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TaskManager.Core.Models;
using TaskManager.Infrastructure.Auth;

namespace TaskManager.Infrastructure.DB
{
  public class TMContext : IdentityDbContext<AppUser>
  {
    public TMContext(DbContextOptions<TMContext> options) : base(options)
    {
      Database.EnsureCreated();
    }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<TaskModel> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<Comment>()
              .HasOne(p=>p.Task)
              .WithMany(p=>p.Comments)
              .HasForeignKey(p=>p.TaskId)
              .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
