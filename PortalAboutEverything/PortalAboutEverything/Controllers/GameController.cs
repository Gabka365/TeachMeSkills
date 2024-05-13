using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Game;

namespace PortalAboutEverything.Controllers
{
    public class GameController : Controller
    {
        private GameRepositories _gameRepositories;

        public GameController(GameRepositories gameRepositories)
        {
            _gameRepositories = gameRepositories;
        }

        public IActionResult Index()
        {
            var gamesViewModel = _gameRepositories
                .GetAll()
                .Select(BuildGameIndexViewModel)
                .ToList();

            return View(gamesViewModel);
        }

        [HttpPost]
        public IActionResult RateTheGame(GameRateViewModel rateViewModel)
        {
            return View(rateViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GameCreateViewModel createGameViewModel)
        {
            var game = new Game
            {
                Name = createGameViewModel.Name,
                Description = createGameViewModel.Description,
                YearOfRelease = createGameViewModel.YearOfRelease,
            };

            _gameRepositories.Create(game);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _gameRepositories.Delete(id);
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

        private GameIndexViewModel BuildGameIndexViewModel(Game game)
            => new GameIndexViewModel
            {
                Id = game.Id,
                Description = game.Description,
                Name = game.Name,
                YearOfRelease = game.YearOfRelease,
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
