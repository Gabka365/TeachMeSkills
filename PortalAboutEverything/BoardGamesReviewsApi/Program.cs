using BoardGamesRiviewsApi.Data;
using BoardGamesRiviewsApi.Data.Models;
using BoardGamesRiviewsApi.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using PortalAboutEverything.Services.Dtos;
using Microsoft.AspNetCore.Mvc;

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
    DateTime dateOfCreationInDate;

    if (dateOfCreation.Contains('/'))
    {
        dateOfCreationInDate = DateTime.ParseExact(dateOfCreation, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
    }
    else
    {
        dateOfCreationInDate = DateTime.ParseExact(dateOfCreation, "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);
    }

    var reviewDataModel = new BoardGameReview()
    {
        UserName = userName,
        UserId = userId,
        DateOfCreation = dateOfCreationInDate,
        Text = text,
        BoardGameId = boardGameId
    };

    repo.Create(reviewDataModel);

    return true;
});
app.MapGet("/updateReview", (BoardGameReviewRepositories repo, /*[FromBody] DtoBoardGameReviewUpdate review,*/
    int id, string text) =>
{
    try
    {
        var reviewDataModel = new BoardGameReview()
        {
            Id = id,
            Text = text
        };

        repo.Update(reviewDataModel);
        return true;
    }
    catch
    {
        return false;
    }
});

app.Run();
