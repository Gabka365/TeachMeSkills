using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Movie;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Data.Model;
using Microsoft.AspNetCore.Authorization;
using PortalAboutEverything.Services;

namespace PortalAboutEverything.Controllers
{
	public class MovieController : Controller
	{
		private MovieRepositories _movieRepositories;
		private MovieReviewRepositories _movieReviewRepositories;
		private AuthService _authService;

		public MovieController(MovieRepositories movieRepositories, 
			MovieReviewRepositories movieReviewRepositories,
			AuthService authService)
		{
			_movieRepositories = movieRepositories;
			_movieReviewRepositories = movieReviewRepositories;
			_authService = authService;
		}

		public IActionResult Index()
		{
			var moviesViewModel = _movieRepositories.GetAllWithReviews().Select(movie => new MovieIndexViewModel
			{
				Id = movie.Id,
				Name = movie.Name,
				Description = movie.Description,
				ReleaseYear = movie.ReleaseYear,
				Director = movie.Director,
				Budget = movie.Budget,
				CountryOfOrigin = movie.CountryOfOrigin,
				Reviews = movie.Reviews.Select(review => new MovieReviewViewModel
				{
					Rate = review.Rate,
					DateOfCreation = review.DateOfCreation,
					Comment = review.Comment,
				}).ToList()
			}).ToList();

			return View(moviesViewModel);
		}

		public IActionResult GiveFeedback(MovieFeedbackViewModel movieFeedbackViewModel)
		{
			return View(movieFeedbackViewModel);
		}

		public IActionResult ShowFeedback(MovieShowFeedbackViewModel showFeedbackViewModel)
		{
			return View(showFeedbackViewModel);
		}

		[HttpGet]
		public IActionResult CreateMovie()
		{
			return View();
		}

		[HttpPost]
		public IActionResult CreateMovie(MovieCreateViewModel movieCreateViewModel)
		{
			var movie = new Movie
			{
				Name = movieCreateViewModel.Name,
				Description = movieCreateViewModel.Description,
				ReleaseYear = movieCreateViewModel.ReleaseYear,
				Director = movieCreateViewModel.Director,
				Budget = movieCreateViewModel.Budget,
				CountryOfOrigin = movieCreateViewModel.CountryOfOrigin,
			};

			_movieRepositories.Create(movie);
			return RedirectToAction("Index");
		}

		public IActionResult DeleteMovie(int id)
		{
			_movieRepositories.Delete(id);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult UpdateMovie(int id)
		{
			var movie = _movieRepositories.Get(id);
			var viewModel = new MovieUpdateViewModel
			{
				Id = movie.Id,
				Name = movie.Name,
				Description = movie.Description,
				ReleaseYear = movie.ReleaseYear,
				Director = movie.Director,
				Budget = movie.Budget,
				CountryOfOrigin = movie.CountryOfOrigin,
			};
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult UpdateMovie(MovieUpdateViewModel movieUpdateViewModel)
		{
			var movie = new Movie
			{
				Id = movieUpdateViewModel.Id,
				Name = movieUpdateViewModel.Name,
				Description = movieUpdateViewModel.Description,
				ReleaseYear = movieUpdateViewModel.ReleaseYear,
				Director = movieUpdateViewModel.Director,
				Budget = movieUpdateViewModel.Budget,
				CountryOfOrigin = movieUpdateViewModel.CountryOfOrigin,
			};

			_movieRepositories.Update(movie);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult MovieAddReview(int id)
		{
			var movie = _movieRepositories.Get(id);
			var viewModel = new MovieAddReviewViewModel
			{
				MovieId = movie.Id,
				Name = movie.Name,
			};
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult MovieAddReview(MovieAddReviewViewModel movieAddReviewViewModel)
		{
			_movieReviewRepositories.AddReviewToMovie(movieAddReviewViewModel.MovieId, 
				movieAddReviewViewModel.Comment, movieAddReviewViewModel.Rate);
			return RedirectToAction("Index");
		}

		[Authorize]
		public IActionResult MoviesFan()
		{
			var userName = _authService.GetUserName();
			var userId = _authService.GetUserId();
			var movies = _movieRepositories.GetFavoriteMoviesByUserId(userId);
			var viewModel = new MoviesFanViewModel
			{
				Name = userName,
				Movies = movies.Select(movie => new MovieIndexViewModel
				{
					Id = movie.Id,
					Name = movie.Name,
					Description = movie.Description,
					ReleaseYear = movie.ReleaseYear,
					Director = movie.Director,
					Budget = movie.Budget,
					CountryOfOrigin = movie.CountryOfOrigin,
				}).ToList(),
			};

			return View(viewModel);
		}
	}
}
