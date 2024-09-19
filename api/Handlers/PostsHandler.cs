using AtScheduler.Contracts.Dtos;
using AtScheduler.Services;
using Microsoft.AspNetCore.Mvc;

namespace AtScheduler.Handlers;

public static class PostsHandler
{
    internal static async Task<IResult> Create([FromBody] SchedulerRequestDto request, [FromServices] IPostService postService)
    {
        var result = await postService.Create(request);
        if (result.IsSuccess)
            return TypedResults.Ok(result.Value);
        
        return TypedResults.BadRequest(result.Error);
    }
    
    internal static IResult Get([FromServices] IPostService postService)
    {
        var result = postService.Get();
        return TypedResults.Ok(result.Value);
    }
    
    internal static async Task<IResult> Delete([FromServices] IPostService postService, [FromRoute] int id)
    {
        await postService.DeleteScheduler(id);
        return TypedResults.NoContent();
    }
}