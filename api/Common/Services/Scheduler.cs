using System.Linq.Expressions;
using System.Text.Json;
using AtScheduler.Common.Interfaces;
using AtScheduler.Services;
using Hangfire;

namespace AtScheduler.Common.Services;

public class Scheduler(IBackgroundJobClient backgroundJobClient) : IScheduler
{
    public void Publish(string jobId, Expression<Action> callBack, string cron)
    {
        RecurringJob.AddOrUpdate(jobId, callBack, cron);
    }

    public void Queue(QueueOptions queueOptions, Expression<Action> callBack)
    {
        if (queueOptions is QueueOptions.CreateSchedule)
        {
            backgroundJobClient.Enqueue(callBack);
        }
    }
    
    public void Delete(string jobId)
    {
        RecurringJob.RemoveIfExists(jobId);
    }
}

public enum QueueOptions
{
    CreateSchedule
}