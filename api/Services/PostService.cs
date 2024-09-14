using AtScheduler.Common.Interfaces;
using AtScheduler.Common.Services;
using AtScheduler.Contracts.Dtos;
using AtScheduler.Contracts.Posts;
using AtScheduler.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AtScheduler.Services;

internal interface ISchedulerService
{
    Task<Result<SchedulerResponseDto, Error>> Create(SchedulerRequestDto request);
    Result<IEnumerable<SchedulerResponseDto>, Error> Get();
    Task DeleteScheduler(int id);
}

public class PostService(IPostRepository postRepository,
    IUnitOfWork unitOfWork,
    IScheduler scheduler,
    IJobService jobService)
    : ISchedulerService
{
    public async Task<Result<SchedulerResponseDto, Error>> Create(SchedulerRequestDto request)
    {
        var newScheduler = Post.Create(request.UserId, request.Content, request.Timer);
        postRepository.Add(newScheduler);
        await unitOfWork.CommitAsync();
        string message = JsonConvert.SerializeObject(newScheduler);
        scheduler.Queue(QueueOptions.CreateSchedule, () => jobService.CreateAndStartJob(message));
        return new SchedulerResponseDto(newScheduler.Id, newScheduler.Content, newScheduler.Timer,
            newScheduler.CreatedAt);
    }
    
    public Result<IEnumerable<SchedulerResponseDto>, Error> Get()
    {
        var schedulers = postRepository
            .Get()
            .AsNoTracking()
            .Select(x => new SchedulerResponseDto(x.Id, x.Content, x.Timer, x.CreatedAt))
            .ToList();

        return schedulers;
    }

    public async Task DeleteScheduler(int id)
    {
        var post = await postRepository.GetByIdAsync(id);
        var job = await jobService.GetBySchedulerId(post.Id);
        scheduler.Delete(job.JobId);
        postRepository.DeleteEntity(post);
        await unitOfWork.CommitAsync();
    }
}