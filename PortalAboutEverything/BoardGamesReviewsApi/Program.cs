using BoardGamesRiviewsApi.Data;
using BoardGamesRiviewsApi.Data.Models;
using BoardGamesRiviewsApi.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
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
app.MapGet("/createReview", (BoardGameReviewRepositories repositories, BoardGameReviewMapper mapper, /*[FromBody] DtoBoardGameReviewCreate review,*/
    string userName, int userId, string dateOfCreation, string text, int boardGameId) =>
{
    //var reviewDataModel = mapper.BuildBoardGameReview(review);

    var dateFormat = dateOfCreation.Contains('/') ? "M/d/yyyy h:mm:ss tt" : "dd.MM.yyyy H:mm:ss";

    var reviewDto = new DtoBoardGameReviewCreate()
    {
        UserName = userName,
        UserId = userId,
        DateOfCreation = DateTime.ParseExact(dateOfCreation, dateFormat, CultureInfo.InvariantCulture),
        Text = text,
        BoardGameId = boardGameId
    };

    var reviewDataModel = mapper.BuildBoardGameReviewFromCreate(reviewDto);
    repositories.Create(reviewDataModel);

    return true;
});
app.MapGet("/updateReview", (BoardGameReviewRepositories repositories, BoardGameReviewMapper mapper, /*[FromBody] DtoBoardGameReviewUpdate review,*/
    int id, string text) =>
{

    var reviewDto = new DtoBoardGameReviewUpdate()
    {
        Id = id,
        Text = text
    };

    var reviewDataModel = mapper.BuildBoardGameReviewFromUpdate(reviewDto);
    repositories.Update(reviewDataModel);

    return true;
});

app.Run();
