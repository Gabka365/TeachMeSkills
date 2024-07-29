using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Movie;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Data.Model;
using Microsoft.AspNetCore.Authorization;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Services;
using PortalAboutEverything.Services.AuthStuff;
using System.Reflection;
using PortalAboutEverything.Services.Apis;

namespace PortalAboutEverything.Controllers
{
    public class MovieController : Controller
    {
        private MovieRepositories _movieRepositories;
        private AuthService _authService;
        private UserRepository _userRepository;
        private PathHelper _pathHelper;
        private HttpMoviesAverageRateApiService _httpMoviesAverageRateApiService;
        private HttpApiSpellService _httpApiSpellService;

        public MovieController(MovieRepositories movieRepositories,
            AuthService authService,
            UserRepository userRepository,
            PathHelper pathHelper,
            HttpMoviesAverageRateApiService httpMoviesAverageRateApiService,
            HttpApiSpellService httpApiSpellService)
        {
            _movieRepositories = movieRepositories;
            _authService = authService;
            _userRepository = userRepository;
            _pathHelper = pathHelper;
            _httpMoviesAverageRateApiService = httpMoviesAverageRateApiService;
            _httpApiSpellService = httpApiSpellService;
        }

        public IActionResult Index()
        {
            var movieStatistics = _movieRepositories.GetMovieStatistic();
            var moviesViewModel = _movieRepositories.GetAll().Select(movie => new MovieIndexViewModel
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                ReleaseYear = movie.ReleaseYear,
                Director = movie.Director,
                Budget = movie.Budget,
                CountryOfOrigin = movie.CountryOfOrigin,
                HasCover = _pathHelper.IsMovieImageExist(movie.Id),
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
            var movieName = _movieRepositories.GetMovieName(id);
            var viewModel = new MovieAddReviewViewModel
            {
                MovieId = id,
                Name = movieName,
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult MovieUpdateReview(int movieId, int reviewId)
        {
            var movieName = _movieRepositories.GetMovieName(movieId);
            var viewModel = new MovieUpdateReviewViewModel
            {
                MovieId = movieId,
                ReviewId = reviewId,
                MovieName = movieName,
            };
            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> MoviesFan()
        {
            var userName = _authService.GetUserName();
            var userId = _authService.GetUserId();
            var movies = _movieRepositories.GetFavoriteMoviesByUserId(userId);
            var moviesId = movies.Select(m => m.Id).ToList();

            var averageRatesTask = _httpMoviesAverageRateApiService.GetAverageRatesAsync(moviesId);
            var spellTask = _httpApiSpellService.GetSpellAsync();

            await Task.WhenAll(averageRatesTask, spellTask);

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
                    AverageRate = averageRatesTask.Result
                    .FirstOrDefault(dto => dto.MovieId == movie.Id)
                    .AverageRate,
                }).ToList(),
                Spell = spellTask.Result.Spell,
                UsageOfSpell = spellTask.Result.Use,
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

        public IActionResult Methods()
        {
            var movieApiController = typeof(ApiControllers.MovieController);
            var methods = movieApiController.GetMethods(BindingFlags.Instance | BindingFlags.Public
                | BindingFlags.DeclaredOnly);
            var methodsArrayModel = new List<MethodViewModel>();

            foreach (var method in methods)
            {
                var methodName = method.Name;
                var methodType = method.ReturnType.Name;
                var parameters = method.GetParameters();
                var parametersArrayModel = new List<MethodParametersViewModel>();
                foreach (var parameter in parameters)
                {
                    var parameterName = parameter.Name;
                    var parameterType = parameter.ParameterType.Name;

                    var parameterModel = new MethodParametersViewModel
                    {
                        ParameterName = parameterName,
                        ParameterType = parameterType,
                    };
                    parametersArrayModel.Add(parameterModel);
                }
                
                var methodModel = new MethodViewModel
                {
                    MethodName = methodName,
                    MethodType = methodType,
                    ParametersInfo = parametersArrayModel,
                };
                methodsArrayModel.Add(methodModel);
            }

            return View(methodsArrayModel);
        }
    }
}
