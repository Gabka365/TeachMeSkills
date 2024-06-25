using BoardGamesRiviewsApi.Data;
using BoardGamesRiviewsApi.Data.Repositories;
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

builder.Services.AddDbContext<ReviewsDbContext>(x => x.UseSqlServer(ReviewsDbContext.CONNECTION_STRING));

builder.Services.AddScoped<BoardGameReviewRepositories>();

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Hello World!");
app.MapGet("/getAll", (BoardGameReviewRepositories repo, int gameId) => repo.GetAllForGame(gameId));

app.Run();
