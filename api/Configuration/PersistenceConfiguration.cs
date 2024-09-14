using AtScheduler.Common.Interfaces;
using AtScheduler.Contracts.Jobs;
using AtScheduler.Contracts.Posts;
using AtScheduler.Contracts.Users;
using AtScheduler.Persistence.Common;
using AtScheduler.Persistence.Data;
using AtScheduler.Persistence.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AtScheduler.Configuration;

public static class PersistenceConfiguration
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql("Host=localhost;Port=5432;User Id=postgres;Password=postgres;Database=agendador", config =>
            {
                config.EnableRetryOnFailure(3, (TimeSpan.FromSeconds(1) * 2), null);
            });
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}