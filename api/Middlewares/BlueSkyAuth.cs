using AtScheduler.Contracts.Users;

public class BlueSkyAuthMiddleware(RequestDelegate next)
{
    private const string DidHeader = "userDid";

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.ToString().ToLower();
        if (path.StartsWith("/api/users") || path.StartsWith("/hangfire"))
        {
            await next(context);
            return;
        }
        
        if (!context.Request.Headers.TryGetValue(DidHeader, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Não autorizado");
            return;
        }

        var userRepository = context.RequestServices.GetService<IUserRepository>();

        var user = await userRepository.FindByDidAsync(extractedApiKey);
        if (user is null)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("usuário não encontrado");
            return;
        }
        
        context.Items["User"] = user;

        await next(context);
    }
}