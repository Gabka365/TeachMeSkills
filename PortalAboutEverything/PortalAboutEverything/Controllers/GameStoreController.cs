using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
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

            var viewModel = new GameStoreIndexViewModel
            {

            };


            return View(viewModel);
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
            _gameStoreRepositories.Save(game);
            return View();
        }
    }
}