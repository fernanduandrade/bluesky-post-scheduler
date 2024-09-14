using AtScheduler.Common.Interfaces;
using AtScheduler.Common.Services;
using AtScheduler.Services;

namespace AtScheduler.Configuration;

public static class ServiceConfiguration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IScheduler, Common.Services.Scheduler>();
        services.AddScoped<ISchedulerService, PostService>();
        services.AddScoped<IJobService, JobService>();
    }
}