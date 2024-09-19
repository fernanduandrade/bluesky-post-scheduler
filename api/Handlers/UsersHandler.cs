using AtScheduler.Contracts.Dtos;
using AtScheduler.Services;
using Microsoft.AspNetCore.Mvc;

namespace AtScheduler.Handlers;

public class UsersHandler
{
    internal static async Task<IResult> Create([FromBody] UserRequestDto request, [FromServices] IUserService userService)
    {
        var result = await userService.Create(request);
        if (result.IsSuccess)
            return TypedResults.Ok(result.Value);
        
        return TypedResults.Unauthorized();
    }
}