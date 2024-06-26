using Microsoft.EntityFrameworkCore;
using NewsTravelingsApi.Data;
using NewsTravelingsApi.Data.Model;
using NewsTravelingsApi.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NewsTravelingsApiDbContext>(x => x.UseSqlServer(NewsTravelingsApiDbContext.CONNECTION_STRING));

builder.Services.AddScoped<NewsRepository>();

var app = builder.Build();

var seed = new Seed();
seed.Fill(app.Services);

app.MapGet("/", () => "Hello World!");

app.MapGet("/DtoLastNews", (NewsRepository repository) => new
{
    Text = repository.LastNews()
});
app.MapPost("/news/add", async (HttpContext context, NewsRepository repository) =>
{
   
    var userId = context.Request.Form["userId"];
    var inputText = context.Request.Form["text"];

    var newNews = new News
    {
        Text = inputText,
        UserId = int.Parse(userId)
    };
    repository.Create(newNews);
});

app.Run();
