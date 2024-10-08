using AtScheduler.Common.Interfaces;
using AtScheduler.Contracts.Jobs;
using AtScheduler.Contracts.Posts;
using AtScheduler.ExternalServices;
using Hangfire;
using Newtonsoft.Json;
using Job = AtScheduler.Contracts.Jobs.Job;

namespace AtScheduler.Services;

public interface IJobService
{
    Task CreateAndStartJob(string message);
    Task<Job> GetBySchedulerId(int id);
}

public class JobService(IBackgroundJobClient backgroundJobClient, IScheduler scheduler, IUnitOfWork unitOfWork,
    IJobRepository jobRepository, IBlueSkyService blueSkyService) : IJobService
{
    public async Task CreateAndStartJob(string message)
    {
        var newScheduler = JsonConvert.DeserializeObject<Post>(message);
        string jobId = Guid.NewGuid().ToString();
        
        var newJob = Job.Create(jobId, newScheduler.Id);
        jobRepository.Add(newJob);
        await unitOfWork.CommitAsync();
        scheduler.Publish(jobId, () => blueSkyService.SendPost(newScheduler.Id), newScheduler.Timer);
    }
    
    public async Task<Job> GetBySchedulerId(int id)
    {
        return await jobRepository.GetBySchedulerId(id);
    }
}