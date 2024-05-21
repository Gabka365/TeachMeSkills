using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Movie;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Controllers
{
	public class MovieController : Controller
	{
		private MovieRepositories _movieRepositories;

		public MovieController(MovieRepositories movieRepositories)
		{
			_movieRepositories = movieRepositories;
		}

		public IActionResult Index()
		{
			var moviesViewModel = _movieRepositories.GetAll().Select(movie => new MovieIndexViewModel
			{
				Id = movie.Id,
				Name = movie.Name,
				Description = movie.Description,
				ReleaseYear = movie.ReleaseYear,
				Director = movie.Director,
				Budget = movie.Budget,
				CountryOfOrigin = movie.CountryOfOrigin,
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


		public IActionResult MovieReview(int id)
		{
			var movie = _movieRepositories.Get(id);
			var viewModel = new MovieReviewViewModel
			{
				Id = movie.Id,
				Name = movie.Name,
			};
			return View(viewModel);
		}

		public IActionResult MovieRate(MovieRateViewModel movieRateViewModel)
		{
			return View(movieRateViewModel);
		}
	}
}
