using BoardGamesRiviewsApi.Data;
using BoardGamesRiviewsApi.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BoardGamesReviewsApi.Middlewares;
using BoardGamesReviewsApi.Dtos;
using BoardGamesReviewsApi.Mappers;

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
builder.Services.AddScoped<BoardGameReviewMapper>();

var app = builder.Build();

app.UseCors();
app.UseMiddleware<AllowAllCorsMiddleware>();

app.MapGet("/", () => "Reviews api");
app.MapGet("/get", (BoardGameReviewRepositories repositories, BoardGameReviewMapper mapper, int id) =>
{
    var review = repositories.Get(id);
    return mapper.BuildBoardGameReviewDto(review);
});
app.MapGet("/getAll", (BoardGameReviewRepositories repositories, int gameId) => repositories.GetAllForGame(gameId));
app.MapGet("/delete", (BoardGameReviewRepositories repositories, int id) => repositories.Delete(id));
app.MapPost("/createReview", (BoardGameReviewRepositories repositories, BoardGameReviewMapper mapper, [FromBody] DtoBoardGameReviewCreate review) =>
{
    var reviewDataModel = mapper.BuildBoardGameReviewFromCreate(review);
    repositories.Create(reviewDataModel);

    return true;
});
app.MapPost("/updateReview", (BoardGameReviewRepositories repositories, BoardGameReviewMapper mapper, [FromBody] DtoBoardGameReviewUpdate review) =>
{
    var reviewDataModel = mapper.BuildBoardGameReviewFromUpdate(review);
    repositories.Update(reviewDataModel);

    return true;
});

app.Run();
