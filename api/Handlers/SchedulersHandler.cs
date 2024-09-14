using AtScheduler.Contracts.Dtos;
using AtScheduler.Services;
using Microsoft.AspNetCore.Mvc;

namespace AtScheduler.Handlers;

public static class SchedulersHandler
{
    internal static async Task<IResult> Create([FromBody] SchedulerRequestDto request, [FromServices] ISchedulerService schedulerService)
    {
        var result = await schedulerService.Create(request);
        if (result.IsSuccess)
            return TypedResults.Ok(result.Value);
        
        return TypedResults.BadRequest(result.Error);
    }
    
    internal static IResult Get([FromServices] ISchedulerService schedulerService)
    {
        var result = schedulerService.Get();
        return TypedResults.Ok(result.Value);
    }
    
    internal static async Task<IResult> Delete([FromServices] ISchedulerService schedulerService, [FromRoute] int id)
    {
        await schedulerService.DeleteScheduler(id);
        return TypedResults.NoContent();
    }
}