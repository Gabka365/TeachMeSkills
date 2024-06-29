using BoardGamesRiviewsApi.Data;
using BoardGamesRiviewsApi.Data.Models;
using BoardGamesRiviewsApi.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using BoardGamesReviewsApi.Middlewares;
using BoardGamesReviewsApi.Dtos;
using Microsoft.VisualBasic;

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
app.UseMiddleware<AllowAllCorsMiddleware>();


app.MapGet("/", () => "Reviews api");
app.MapGet("/get", (BoardGameReviewRepositories repo, int id) =>
{
    var review = repo.Get(id);
    return new DtoBoardGameReview()
    {
        Id = review.Id,
        UserName = review.UserName,
        UserId = review.UserId,
        DateOfCreation = review.DateOfCreation,
        Text = review.Text,
        BoardGameId = review.BoardGameId
    };
});
app.MapGet("/getAll", (BoardGameReviewRepositories repo, int gameId) => repo.GetAllForGame(gameId));
app.MapGet("/delete", (BoardGameReviewRepositories repo, int id) => repo.Delete(id));
app.MapGet("/createReview", (BoardGameReviewRepositories repo, /*[FromBody] DtoBoardGameReviewCreate review,*/
    string userName, int userId, string dateOfCreation, string text, int boardGameId) =>
{
    //var reviewDataModel = new BoardGameReview()
    //{
    //    UserName = review.UserName,
    //    UserId = review.UserId,
    //    DateOfCreation = review.DateOfCreation,
    //    Text = review.Text,
    //    BoardGameId = review.BoardGameId
    //};

    var dateFormat = dateOfCreation.Contains('/') ? "M/d/yyyy h:mm:ss tt" : "dd.MM.yyyy H:mm:ss";

    var reviewDataModel = new BoardGameReview()
    {
        UserName = userName,
        UserId = userId,
        DateOfCreation = DateTime.ParseExact(dateOfCreation, dateFormat, CultureInfo.InvariantCulture),
        Text = text,
        BoardGameId = boardGameId
    };

    repo.Create(reviewDataModel);

    return true;
});
app.MapGet("/updateReview", (BoardGameReviewRepositories repo, /*[FromBody] DtoBoardGameReviewUpdate review,*/
    int id, string text) =>
{

    var reviewDataModel = new BoardGameReview()
    {
        Id = id,
        Text = text
    };

    repo.Update(reviewDataModel);

    return true;
});

app.Run();
