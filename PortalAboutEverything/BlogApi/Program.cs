using BlogApi.Data;
using BlogApi.Data.Repositories;
using BlogApi.Hubs;
using BlogApi.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.SetIsOriginAllowed(x => true);
        p.AllowCredentials();
    });
});

builder.Services.AddDbContext<BlogApiDbContext>(x => x.UseSqlServer(BlogApiDbContext.CONNECTION_STRING));
builder.Services.AddScoped<BlogApiRepositories>();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors();

app.UseMiddleware<CustomAuthMiddleware>();

app.MapHub<BlogCommentHub>("/hubs/BlogComment");

app.MapGet("/", () => "Hello World!");

app.MapGet("/para", () => "You are here");

app.MapGet("/getAllComments", (BlogApiRepositories repo) => repo.GetAllComments()); // дописать вызов метода

app.Run();
