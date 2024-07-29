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

app.MapPost("/updateReview", (
	[FromBody] ReviewModel review,
    MovieReviewRepositories movieRepository) =>
{
    var movieReviewDataModel = new MovieReview
    {
        Id = review.ReviewId,
        Rate = review.Rate,
        Comment = review.Comment,
    };
    movieRepository.Update(movieReviewDataModel);
});

app.MapPost("/deleteAllReviewsByMovieId", (
	int movieId,
	MovieReviewRepositories movieRepository) =>
{
	movieRepository.DeleteAllReviewsByMovieId(movieId);
});

app.MapGet("/getAmountOfReviewsByMovie", (
    int movieId,
    MovieReviewRepositories movieRepository) =>
{
    return movieRepository.GetAmountOfReviewsByMovie(movieId);
});

app.MapGet("/deleteReview", (
    int reviewId,
    MovieReviewRepositories movieRepository) =>
{
    movieRepository.Delete(reviewId);
    return true;
});

app.MapGet("/findReviewInfo", (
    int reviewId,
	MovieReviewRepositories movieRepository) =>
{
	return movieRepository.Get(reviewId);
});

app.MapPost("/getAverageRateOfMovies", ([FromBody] List<int> moviesId, MovieReviewRepositories repository) =>
{
    var moviesAverageRateList = new List<AverageRateOfMovie>();
    foreach (int movieId in moviesId)
    {
        var averageRate =  repository.GetAverageRateOfMovie(movieId);
        moviesAverageRateList.Add(new AverageRateOfMovie {
        AverageRate = averageRate,
        MovieId = movieId
        });
    }
    return moviesAverageRateList;
});

app.Run();
