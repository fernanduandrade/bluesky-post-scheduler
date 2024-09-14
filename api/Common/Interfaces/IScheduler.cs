using System.Linq.Expressions;
using AtScheduler.Common.Services;

namespace AtScheduler.Common.Interfaces;

public interface IScheduler
{
    void Publish(string jobId, Expression<Action> callBack, string cron);
    void Queue(QueueOptions queueOptions, Expression<Action> callBack);
    void Delete(string jobId);
}