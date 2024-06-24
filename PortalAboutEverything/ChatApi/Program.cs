using ChatApi.FakeDb;
using ChatApi.Hubs;
using ChatApi.Middlewares;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//register service into DI container here
builder.Services.AddScoped<ChatRepositoryFake>();

// Who we behave with request from ANOTHER domains.
builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        //p.AllowAnyOrigin();
        p.SetIsOriginAllowed(x => true);
        p.AllowCredentials();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();
// Middleware service add here
app.UseCors();

app.UseMiddleware<CustomAuthMiddleware>();

app.MapHub<ChatHub>("/hubs/chat");

app.MapGet("/", () => "Hello World!");

app.MapGet("/getLastMessages", (ChatRepositoryFake repo) => repo.GetLast5Messages());

app.MapGet("/add", (
    [FromQuery] string name,
    [FromQuery] string text,
    ChatRepositoryFake repo) =>
{
    repo.AddMessage(name, text);
    return true;
});

app.Run();
