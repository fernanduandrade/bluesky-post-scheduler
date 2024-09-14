using AtScheduler.Configuration;
using AtScheduler.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence();
builder.Services.AddHanfireConfiguration();
builder.Services.AddServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var schedulers = app.MapGroup("api/schedulers");

schedulers.MapPost("", SchedulersHandler.Create);
schedulers.MapGet("", SchedulersHandler.Get);
schedulers.MapDelete("{id}", SchedulersHandler.Delete);

app.AddHangireDash();
app.UseHttpsRedirection();
app.Run();