using Hangfire;

namespace AtScheduler.Configuration;

public static class HangfireConfiguration
{
    public static void AddHanfireConfiguration(this IServiceCollection services)
    {
        services.AddHangfire(config => config.UseInMemoryStorage());
        services.AddHangfireServer();
    }

    public static void AddHangireDash(this WebApplication app)
    {
        app.UseHangfireDashboard();
    }
}