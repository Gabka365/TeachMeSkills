using BoardGameOfDayApi.Dtos;
using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data;
using PortalAboutEverything.Data.Repositories;

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

builder.Services.AddDbContext<PortalDbContext>(x => x.UseSqlServer(PortalDbContext.CONNECTION_STRING));
builder.Services.AddScoped<BoardGameRepositories>();

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Board game of day api");
app.MapGet("/getBoardGameOfDay", (BoardGameRepositories repository) => {
    var allId = repository.GetAllId();
    var random = new Random();
    var index = random.Next(allId.Count);

    var boardGame = repository.Get(allId[index]);
    var dto = new DtoBoardGameOfDay
    {
        Id = boardGame.Id,
        Title = boardGame.Title,
        Price = boardGame.Price
    };

    return dto;
});

app.Run();
