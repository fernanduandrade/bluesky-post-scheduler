using Hangfire;
using Hangfire.Dashboard;

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
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[] { new HangfireAuthorizationFilter() }
        });
    }
}

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        // Allow all users to access the dashboard (can restrict based on user roles or other criteria)
        return true;
    }
}