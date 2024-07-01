using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesReviewsApi.Data;
using MoviesReviewsApi.Data.Model;
using MoviesReviewsApi.Data.Repositories;
using MoviesReviewsApi.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MoviesReviewsDbContext>(x => x.UseSqlServer(MoviesReviewsDbContext.CONNECTION_STRING));
builder.Services.AddScoped<MovieReviewRepositories>();

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

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "MovieReview");

app.MapPost("/addReview", (
    [FromBody] ReviewModel review,
    MovieReviewRepositories movieRepository) =>
{
    var movieReviewDataModel = new MovieReview
    {
        Rate = review.Rate,
        DateOfCreation = DateTime.Now,
        Comment = review.Comment,
        MovieId = review.MovieId
	};
    movieRepository.Create(movieReviewDataModel);
});

app.MapGet("/findReviewsByMovieId", (
    int movieId,
    MovieReviewRepositories movieRepository) =>
{
    return movieRepository.FindReviewsByMovieId(movieId);
});

app.MapGet("/updateReview", (
	[FromBody] ReviewModel review,
    MovieReviewRepositories movieRepository) =>
{
    var movieReviewDataModel = new MovieReview
    {
        Id = review.Id,
        Rate = review.Rate,
        Comment = review.Comment,
    };
    movieRepository.Update(movieReviewDataModel);
});

app.MapGet("/deleteReview", (
    int reviewId,
    MovieReviewRepositories movieRepository) =>
{
    movieRepository.Delete(reviewId);
    return true;
});

app.Run();
