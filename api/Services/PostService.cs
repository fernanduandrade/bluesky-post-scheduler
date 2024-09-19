using AtScheduler.Common.Interfaces;
using AtScheduler.Common.Services;
using AtScheduler.Contracts.Dtos;
using AtScheduler.Contracts.Posts;
using AtScheduler.Contracts.Users;
using AtScheduler.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AtScheduler.Services;

public interface IPostService
{
    Task<Result<PostResponseDto, Error>> Create(PostRequestDto request);
    Result<IEnumerable<PostResponseDto>, Error> Get();
    Task DeleteScheduler(int id);
}

public class PostService(IPostRepository postRepository,
    IUnitOfWork unitOfWork,
    IScheduler scheduler,
    IJobService jobService,
    IHttpContextAccessor httpContextAccessor)
    : IPostService
{
    public async Task<Result<PostResponseDto, Error>> Create(PostRequestDto request)
    {
        var user = httpContextAccessor.HttpContext.Items["User"] as User;
        var newScheduler = Post.Create(user.Id, request.Content, request.Timer);
        postRepository.Add(newScheduler);
        await unitOfWork.CommitAsync();
        string message = JsonConvert.SerializeObject(newScheduler);
        scheduler.Queue(QueueOptions.CreateSchedule, () => jobService.CreateAndStartJob(message));
        return new PostResponseDto(newScheduler.Id, newScheduler.Content, newScheduler.Timer,
            newScheduler.CreatedAt);
    }
    
    public Result<IEnumerable<PostResponseDto>, Error> Get()
    {
        var schedulers = postRepository
            .Get()
            .AsNoTracking()
            .Select(x => new PostResponseDto(x.Id, x.Content, x.Timer, x.CreatedAt))
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