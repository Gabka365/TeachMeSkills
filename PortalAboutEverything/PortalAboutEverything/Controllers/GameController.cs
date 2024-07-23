using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Game;
using PortalAboutEverything.Services;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Controllers
{
    public class GameController : Controller
    {
        private GameRepositories _gameRepositories;
        private BoardGameReviewRepositories _boardGameReviewRepositories;
        private AuthService _authService;
        private PathHelper _pathHelper;

        public GameController(GameRepositories gameRepositories,
            BoardGameReviewRepositories boardGameReviewRepositories,
            AuthService authService,
            PathHelper pathBuilder)
        {
            _gameRepositories = gameRepositories;
            _boardGameReviewRepositories = boardGameReviewRepositories;
            _authService = authService;
            _pathHelper = pathBuilder;
        }

        public IActionResult Index()
        {
            var gamesViewModel = _gameRepositories
                .GetAllWithReviews()
                .Select(BuildGameIndexViewModel)
                .ToList();

            var viewModel = new IndexViewModel()
            {
                Games = gamesViewModel,
                CanCreateGame = _authService.GetUserPermission().HasFlag(Permission.CanCreateGame),
                CanDeleteGame = _authService.GetUserPermission().HasFlag(Permission.CanDeleteGame)
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddReview(AddGameReviewViewModel viewModel)
        {
            _boardGameReviewRepositories.AddReviewToGame(viewModel.GameId, viewModel.Text);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RateTheGame(GameRateViewModel rateViewModel)
        {
            return View(rateViewModel);
        }

        [HttpGet]
        [Authorize]
        [HasPermission(Permission.CanCreateGame)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [HasPermission(Permission.CanCreateGame)]
        public IActionResult Create(GameCreateViewModel createGameViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createGameViewModel);
            }

            var game = new Game
            {
                Name = createGameViewModel.Name,
                Description = createGameViewModel.Description,
                YearOfRelease = createGameViewModel.YearOfRelease,
            };

            _gameRepositories.Create(game);

            var path = _pathHelper.GetPathToGameCover(game.Id);
            using (var fs = new FileStream(path, FileMode.Create))// file size 0 bite
            {
                createGameViewModel.Cover.CopyTo(fs);// full file size
            }

            return RedirectToAction("Index");
        }

        [HasPermission(Permission.CanDeleteGame)]
        public IActionResult Delete(int id)
        {
            _gameRepositories.Delete(id);

            var path = _pathHelper.GetPathToGameCover(id);
            System.IO.File.Delete(path);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var game = _gameRepositories.Get(id);
            var viewModel = BuildGameUpdateViewModel(game);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(GameUpdateViewModel viewModel)
        {
            var game = new Game
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                YearOfRelease = viewModel.YearOfRelease,
            };
            _gameRepositories.Update(game);

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Gamer()
        {
            var userName = _authService.GetUserName();

            var userId = _authService.GetUserId();
            var games = _gameRepositories.GetFavoriteGamesByUserId(userId);

            var viewModel = new GamerViewModel
            {
                Name = userName,
                Games = games
                    .Select(BuildGameUpdateViewModel)
                    .ToList(),
            };

            return View(viewModel);
        }

        private GameIndexViewModel BuildGameIndexViewModel(Game game)
            => new GameIndexViewModel
            {
                Id = game.Id,
                Description = game.Description,
                Name = game.Name,
                YearOfRelease = game.YearOfRelease,
                Reviews = game
                    .Reviews
                    .Select(BuildGameReviewViewModel)
                    .ToList(),
                HasCover = _pathHelper.IsGameCoverExist(game.Id)
            };

        private GameReviewViewModel BuildGameReviewViewModel(BoardGameReview review)
            => new GameReviewViewModel
            {
                Text = review.Text,
                DateOfCreation = review.DateOfCreation,
            };

        private GameUpdateViewModel BuildGameUpdateViewModel(Game game)
            => new GameUpdateViewModel
            {
                Id = game.Id,
                Description = game.Description,
                Name = game.Name,
                YearOfRelease = game.YearOfRelease,
            };
    }
}
