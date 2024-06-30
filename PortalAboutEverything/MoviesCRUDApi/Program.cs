using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesReviewsApi.Data;
using MoviesReviewsApi.Data.Model;
using MoviesReviewsApi.Data.Repositories;

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

//app.MapGet("/addReview", (
//    int rate,
//	//    string comment,
//	//    int movieId,
//	HttpContext context,
//    MovieReviewRepositories movieRepository) =>
//{
//	var rate = context.Request.Form["rate"];
//	var comment = context.Request.Form["comment"];
//	var movieId = context.Request.Form["movieId"];

//	var movieReviewDataModel = new MovieReview
//    {
//        Rate = int.Parse(rate),
//        DateOfCreation = DateTime.Now,
//        Comment = comment,
//        MovieId = int.Parse(movieId)
//    };
//    movieRepository.Create(movieReviewDataModel);
//});



app.MapGet("/addReview", (
    [FromQuery]string rate,
	[FromQuery] string comment,
	[FromQuery] string movieId,
    MovieReviewRepositories movieRepository) =>
{
    var movieReviewDataModel = new MovieReview
    {
        Rate = int.Parse(rate),
        DateOfCreation = DateTime.Now,
        Comment = comment,
        MovieId = int.Parse(movieId)
	};
    movieRepository.Create(movieReviewDataModel);
    return true;
	//return Ok(new { success = true });
});

app.MapGet("/updateReview", (
    [FromQuery] int rate,
    [FromQuery] string comment,
    [FromQuery] int reviewId,
    MovieReviewRepositories movieRepository) =>
{
    var movieReviewDataModel = new MovieReview
    {
        Id = reviewId,
        Rate = rate,
        Comment = comment,
    };
    movieRepository.Update(movieReviewDataModel);
});

app.MapGet("/deleteReview", (
    [FromQuery] int reviewId,
    MovieReviewRepositories movieRepository) =>
{
    movieRepository.Delete(reviewId);
});

app.Run();
