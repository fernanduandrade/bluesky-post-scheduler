using AtScheduler.Common.Interfaces;
using AtScheduler.Common.Services;
using AtScheduler.ExternalServices;
using AtScheduler.Services;

namespace AtScheduler.Configuration;

public static class ServiceConfiguration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IScheduler, Scheduler>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBlueSkyService, BlueSkyService>();
    }
}