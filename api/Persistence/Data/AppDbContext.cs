using AtScheduler.Contracts.Jobs;
using AtScheduler.Contracts.Posts;
using AtScheduler.Contracts.Users;
using Microsoft.EntityFrameworkCore;

namespace AtScheduler.Persistence.Data;

public class AppDbContext : DbContext
{
    public DbSet<Post> Schedulers => Set<Post>();
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<User> Users => Set<User>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}