using ChatApi.FakeDb;
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
        p.AllowAnyOrigin();
    });
});

var app = builder.Build();
// Middleware service add here
app.UseCors();

app.MapGet("/", () => "Hello World!");

app.MapGet("/getAll", (ChatRepositoryFake repo) => repo.GetAll());

app.MapGet("/add", (
    [FromQuery]string name, 
    [FromQuery] string text, 
    ChatRepositoryFake repo) =>
{
    repo.AddMessage(name, text);
    return true;
});

app.Run();
