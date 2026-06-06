using GameBacklogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBacklogApi.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  public DbSet<User> Users { get; set; }
  public DbSet<Game> Games { get; set; }
  public DbSet<UserGameBacklog> UserGameBacklogs { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // EF Core needs to use a combination of UserId and GameId as pk
    modelBuilder.Entity<UserGameBacklog>().HasKey(ugb => new { ugb.UserId, ugb.GameId });
  }

}