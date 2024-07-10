using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Game;
using PortalAboutEverything.Models.Game.Api;
using PortalAboutEverything.Models.User;

namespace PortalAboutEverything.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class GamesController : Controller
    {
        private GameRepositories _gameRepositories;

        public GamesController(GameRepositories gameRepositories)
        {
            _gameRepositories = gameRepositories;
        }

        public List<GameIndexViewModel> GetAll()
        {
            var gamesViewModel = _gameRepositories
                .GetAllWithReviews()
                .Select(BuildGameIndexViewModel)
                .ToList();
            return gamesViewModel;
        }

        [HttpPost]
        public bool Create(CreateGameViewModel game)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            var gameDb = new Game
            {
                Name = game.Name,
                Description = game.Name,
                YearOfRelease = game.YearOfRelease
            };

            _gameRepositories.Create(gameDb);

            return true;
        }

        public void Remove(int id)
            => _gameRepositories.Delete(id);

        public GameIndexViewModel Get(int id)
            => BuildGameIndexViewModel(_gameRepositories.Get(id));

        private GameIndexViewModel BuildGameIndexViewModel(Game game)
            => new GameIndexViewModel
            {
                Id = game.Id,
                Description = game.Description,
                Name = game.Name,
                YearOfRelease = game.YearOfRelease,
            };
    }
}
