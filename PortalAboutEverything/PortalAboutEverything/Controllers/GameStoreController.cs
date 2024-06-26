using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Models.GameStore;
using PortalAboutEverything.Services.AuthStuff;
using PortalAboutEverything.Services;

namespace PortalAboutEverything.Controllers
{
    public class GameStoreController : Controller
    {
        private GameStoreRepositories _gameStoreRepositories;
        private AuthService _authService;
        private PathHelper _pathHelper;

        public GameStoreController(GameStoreRepositories gameStoreRepositories,
            AuthService authService,
            PathHelper pathHelper)
        {
            _gameStoreRepositories = gameStoreRepositories;
            _authService = authService;
            _pathHelper = pathHelper;
        }

        public IActionResult Index()
        {
            var gamesViewModel = _gameStoreRepositories
                .GetAll()
                .Select(BuildGameStoreIndexViewModel)
                .ToList();

            var viewModel = new IndexGameViewModel()
            {
                Games = gamesViewModel,
                CanCreateGameInGameStore = _authService.GetUserPermission().HasFlag(Permission.CanCreateGameInGameStore),
                CanDeleteGameInGameStore = _authService.GetUserPermission().HasFlag(Permission.CanDeleteGameInGameStore),
                CanUpdateGameInGameStore = _authService.GetUserPermission().HasFlag(Permission.CanUpdateGameInGameStore),
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult RateTheGameStore(RateTheGameStoreViewModel rateTheGameStoreViewModel)
        {
            return View(rateTheGameStoreViewModel);
        }

        [HttpGet]
        [Authorize]
        [HasPermission(Permission.CanCreateGameInGameStore)]
        public IActionResult CreateGame()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [HasPermission(Permission.CanCreateGameInGameStore)]
        public IActionResult CreateGame(CreateGameStoreViewModel createGameStoreViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createGameStoreViewModel);
            }
            var game = new GameStore
            {
                GameName = createGameStoreViewModel.GameName,
                Developer = createGameStoreViewModel.Developer,
                YearOfRelease = createGameStoreViewModel.YearOfRelease,

            };

            _gameStoreRepositories.Create(game);

            var path = _pathHelper.GetPathToGameStoreCover(game.Id);
            using(var fs = new FileStream(path, FileMode.Create))
            {
                createGameStoreViewModel.Cover.CopyTo(fs);
            }
            return RedirectToAction("Index");
        }

        [HasPermission(Permission.CanDeleteGameInGameStore)]
        public IActionResult Delete(int id)
        {
            var path = _pathHelper.GetPathToGameStoreCover(id);
            _gameStoreRepositories.Delete(id);
            System.IO.File.Delete(path);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [HasPermission(Permission.CanUpdateGameInGameStore)]
        public IActionResult Update(int id)
        {
            var game = _gameStoreRepositories.Get(id);
            var viewModel = BuildGameStoreUpdateViewModel(game);
            return View(viewModel);
        }

        [HttpPost]
        [HasPermission(Permission.CanUpdateGameInGameStore)]
        public IActionResult Update(GameStoreUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var game = new GameStore
            {
                Id = viewModel.Id,
                GameName = viewModel.GameName,
                YearOfRelease = viewModel.YearOfRelease,
                Developer = viewModel.Developer,
            };
            _gameStoreRepositories.Update(game);

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult GamerGameStore()
        {
            var userName = _authService.GetUserName();
            var userId = _authService.GetUserId();
            var games = _gameStoreRepositories.GetGamesByUserId(userId);

            var viewModel = new GamerGameStoreViewModel
            {
                Name = userName,
                Games = games.Select(BuildGameStoreUpdateViewModel).ToList(),
            };
            return View(viewModel);
        }

        private GameStoreIndexViewModel BuildGameStoreIndexViewModel(GameStore game)
        {
            return new GameStoreIndexViewModel
            {
                Id = game.Id,
                GameName = game.GameName,
                YearOfRelease = game.YearOfRelease,
                Developer = game.Developer,
                HasCover = _pathHelper.IsGameStoreCoverExist(game.Id),
            };
        }

        private GameStoreUpdateViewModel BuildGameStoreUpdateViewModel(GameStore game)
            => new GameStoreUpdateViewModel
            {
                Id = game.Id,
                GameName = game.GameName,
                YearOfRelease = game.YearOfRelease,
                Developer = game.Developer,
            };
    }
}