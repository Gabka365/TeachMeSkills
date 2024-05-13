using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Game;
using PortalAboutEverything.Models.GameStore;

namespace PortalAboutEverything.Controllers
{
    public class GameStoreController : Controller
    {
        private GameStoreRepositories _gameStoreRepositories;

        public GameStoreController(GameStoreRepositories gameStoreRepositories)
        {
            _gameStoreRepositories = gameStoreRepositories;
        }

        public IActionResult Index()
        {
            var gamesViewModel = _gameStoreRepositories
                .GetAll()
                .Select(BuildGameStoreIndexViewModel)
                .ToList();

            return View(gamesViewModel);
        }

        [HttpPost]
        public IActionResult RateTheGameStore(RateTheGameStoreViewModel rateTheGameStoreViewModel)
        {
            return View(rateTheGameStoreViewModel);
        }

        [HttpGet]
        public IActionResult CreateGame()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateGame(CreateGameStoreViewModel createGameStoreViewModel)
        {
            var game = new GameStore
            {
                GameName = createGameStoreViewModel.GameName,
                Developer = createGameStoreViewModel.Developer,
                YearOfRelease = createGameStoreViewModel.YearOfRelease,

            };
            _gameStoreRepositories.Create(game);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _gameStoreRepositories.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var game = _gameStoreRepositories.Get(id);
            var viewModel = BuildGameStoreUpdateViewModel(game);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(GameStoreUpdateViewModel viewModel)
        {
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

        private GameStoreIndexViewModel BuildGameStoreIndexViewModel(GameStore game)
        {
            return new GameStoreIndexViewModel
            {
                Id = game.Id,
                GameName = game.GameName,
                YearOfRelease = game.YearOfRelease,
                Developer = game.Developer,
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