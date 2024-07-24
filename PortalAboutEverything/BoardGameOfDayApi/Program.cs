using BoardGameOfDayApi.Dtos;
using BoardGameOfDayApi.Services;
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

builder.Services.AddSingleton<CacheService>();

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Board game of day api");
app.MapGet("/getBoardGameOfDay", (BoardGameRepositories repository, CacheService cache) => {

    var id = cache.GetBoardGameOfDayId(repository.GetAllId());
    var boardGame = repository.Get(id);
    var dto = new DtoBoardGameOfDay
    {
        Id = boardGame.Id,
        Title = boardGame.Title,
        Price = boardGame.Price
    };

    return dto;
});

app.Run();
