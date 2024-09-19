using AtScheduler.Configuration;
using AtScheduler.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddPersistence();
builder.Services.AddHanfireConfiguration();
builder.Services.AddServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var posts = app.MapGroup("api/posts");
var users = app.MapGroup("api/users");

posts.MapPost("", PostsHandler.Create);
posts.MapGet("", PostsHandler.Get);
posts.MapDelete("{id}", PostsHandler.Delete);
users.MapPost("", UsersHandler.Create);
app.UseMiddleware<BlueSkyAuthMiddleware>();

app.AddHangireDash();
app.UseHttpsRedirection();
app.Run();