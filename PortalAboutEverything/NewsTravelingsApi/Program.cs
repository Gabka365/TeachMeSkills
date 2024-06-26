using Microsoft.EntityFrameworkCore;
using NewsTravelingsApi.Data;
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

app.Run();
