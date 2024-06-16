using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Movie;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Data.Model;
using Microsoft.AspNetCore.Authorization;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Services;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Controllers
{
    public class MovieController : Controller
    {
        private MovieRepositories _movieRepositories;
        private MovieReviewRepositories _movieReviewRepositories;
        private AuthService _authService;
        private UserRepository _userRepository;
        private PathHelper _pathHelper;

        public MovieController(MovieRepositories movieRepositories,
            MovieReviewRepositories movieReviewRepositories,
            AuthService authService,
            UserRepository userRepository,
            PathHelper pathHelper)
        {
            _movieRepositories = movieRepositories;
            _movieReviewRepositories = movieReviewRepositories;
            _authService = authService;
            _userRepository = userRepository;
            _pathHelper = pathHelper;
        }

        public IActionResult Index()
        {
            var movieStatistics = _movieRepositories.GetMovieStatistic();
            var moviesViewModel = _movieRepositories.GetAllWithReviews().Select(movie => new MovieIndexViewModel
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                ReleaseYear = movie.ReleaseYear,
                Director = movie.Director,
                Budget = movie.Budget,
                CountryOfOrigin = movie.CountryOfOrigin,
                HasCover = _pathHelper.IsMovieImageExist(movie.Id),
                Reviews = movie.Reviews.Select(review => new MovieReviewViewModel
                {
                    Rate = review.Rate,
                    DateOfCreation = review.DateOfCreation,
                    Comment = review.Comment,
                }).ToList()
            }).ToList();

            var viewModel = new IndexMovieAdminViewModel()
            {
                Movies = moviesViewModel,
                MovieStatistics = movieStatistics.ToList(),
            };

            if (_authService.IsAuthenticated())
            {
                viewModel.IsMovieAdmin = _authService.HasRoleOrHigher(UserRole.MovieAdmin);
            }
            else
            {
                viewModel.IsMovieAdmin = false;
            }

            return View(viewModel);
        }

        [Authorize]
        public IActionResult GiveFeedback(MovieFeedbackViewModel movieFeedbackViewModel)
        {
            return View(movieFeedbackViewModel);
        }

        public IActionResult ShowFeedback(MovieShowFeedbackViewModel showFeedbackViewModel)
        {
            return View(showFeedbackViewModel);
        }

        [HttpGet]
        [Authorize]
        [HasPermission(Permission.CanCreateMovie)]
        public IActionResult CreateMovie()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [HasPermission(Permission.CanCreateMovie)]
        public IActionResult CreateMovie(MovieCreateViewModel movieCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(movieCreateViewModel);
            }

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

            if (movieCreateViewModel.MovieImage == null)
            {
                return RedirectToAction("Index");
            }

            var path = _pathHelper.GetPathToMovieImage(movie.Id);
            using (var fs = new FileStream(path, FileMode.Create))
            {
                movieCreateViewModel.MovieImage.CopyTo(fs);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [HasPermission(Permission.CanDeleteMovie)]
        public IActionResult DeleteMovie(int id)
        {
            _movieRepositories.Delete(id);

            var path = _pathHelper.GetPathToMovieImage(id);
            System.IO.File.Delete(path);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        [HasPermission(Permission.CanUpdateMovie)]
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
        [Authorize]
        [HasPermission(Permission.CanUpdateMovie)]
        public IActionResult UpdateMovie(MovieUpdateViewModel movieUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(movieUpdateViewModel);
            }

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
        [Authorize]
        [HasPermission(Permission.CanLeaveReviewForMovie)]
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
        [Authorize]
        [HasPermission(Permission.CanLeaveReviewForMovie)]
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

        [HttpPost]
        [Authorize]
        public IActionResult AddMovieToMoviesFan(AddMovieToMoviesFanViewModel viewModel)
        {
            var userId = _authService.GetUserId();
            var movie = _movieRepositories.Get(viewModel.MovieId);
            _userRepository.AddMovieToMoviesFan(movie, userId);
            return RedirectToAction("MoviesFan");
        }

        public IActionResult DeleteMovieFromMovieFan(int movieId)
        {
            var userId = _authService.GetUserId();
            var movie = _movieRepositories.Get(movieId);
            _userRepository.DeleteMovieFromMoviesFan(movie, userId);

            return RedirectToAction("MoviesFan");
        }
    }
}
