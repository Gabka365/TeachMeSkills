using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.BoardGame;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;

namespace PortalAboutEverything.Controllers
{
    public class BoardGameController : Controller
    {
        private BoardGameRepositories _gameRepositories;
        private BoardGameReviewRepositories _reviewRepositories;

        public BoardGameController(BoardGameRepositories gameRepositories, BoardGameReviewRepositories reviewRepositories)
        {
            _gameRepositories = gameRepositories;
            _reviewRepositories = reviewRepositories;
        }

        public IActionResult Index()
        {
            List<BoardGameIndexViewModel> indexViewModel = _gameRepositories
                .GetAll()
                .Select(BuildBoardGameIndexViewModel)
                .ToList();

            return View(indexViewModel);
        }

        [HttpGet]
        public IActionResult CreateBoardGame()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBoardGame(BoardGameCreateViewModel boardGameViewModel)
        {
            BoardGame game = BuildBoardGameDataModelFromCreate(boardGameViewModel);

            _gameRepositories.Create(game);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateBoardGame(int id)
        {
            BoardGame boardGameForUpdate = _gameRepositories.Get(id);
            BoardGameUpdateViewModel viewModel = BuildBoardGameUpdateDataModel(boardGameForUpdate);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateBoardGame(BoardGameUpdateViewModel boardGameViewModel)
        {
            BoardGame updatedReview = BuildBoardGameDataModelFromUpdate(boardGameViewModel);
            _gameRepositories.Update(updatedReview);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteBoardGame(int id)
        {
            _gameRepositories.Delete(id);

            return RedirectToAction("Index");
        }

        public IActionResult BoardGame(int id)
        {
            BoardGame gameViewModel = _gameRepositories.Get(id);
            BoardGameViewModel viewModel = BuildBoardGameViewModel(gameViewModel);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateReview(int gameId)
        {
            BoardGameCreateReviewViewModel createReviewViewModel =
                new BoardGameCreateReviewViewModel
                {
                    BoardGameId = gameId,
                    BoardGameName = _gameRepositories.Get(gameId).Title,
                };

            return View(createReviewViewModel);
        }

        [HttpPost]
        public IActionResult CreateReview(BoardGameCreateReviewViewModel boardGameReviewViewModel)
        {
            BoardGameReview review = BuildBoardGameRewievDataModelFromCreate(boardGameReviewViewModel);

            _reviewRepositories.Create(review);
            return RedirectToAction("BoardGame");
        }

        [HttpGet]
        public IActionResult UpdateReview(int id)
        {
            BoardGameReview reviewForUpdate = _reviewRepositories.Get(id);
            BoardGameUpdateReviewViewModel viewModel = BuildBoardGameUpdateRewievDataModel(reviewForUpdate);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateReview(BoardGameUpdateReviewViewModel boardGameReviewViewModel)
        {
            BoardGameReview updatedReview = BuildBoardGameRewievDataModelFromUpdate(boardGameReviewViewModel);
            _reviewRepositories.Update(updatedReview);

            return RedirectToAction("BoardGame");
        }

        public IActionResult DeleteReview(int id)
        {
            _reviewRepositories.Delete(id);

            return RedirectToAction("BoardGame");
        }

        private BoardGameViewModel BuildBoardGameViewModel(BoardGame game)
        {
            List<BoardGameReviewViewModel> reviewViewModels = new();
            if (game.Reviews != null)
            {
                foreach (var review in game.Reviews)
                {
                    reviewViewModels.Add(BuildBoardGameRewievViewModel(review));
                }
            }

            return new BoardGameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                MiniTitle = game.MiniTitle,
                Description = game.Description,
                Essence = game.Essence,
                Tags = game.Tags,
                Price = game.Price,
                ProductCode = game.ProductCode,
                Reviews = reviewViewModels
            };
        }

        private BoardGame BuildBoardGameDataModelFromCreate(BoardGameCreateViewModel gameViewModel)
            => new BoardGame
            {
                Title = gameViewModel.Title,
                MiniTitle = gameViewModel.MiniTitle,
                Description = gameViewModel.Description,
                Essence = gameViewModel.Essence,
                Tags = gameViewModel.Tags,
                Price = gameViewModel.Price,
                ProductCode = gameViewModel.ProductCode,
            };

        private BoardGameUpdateViewModel BuildBoardGameUpdateDataModel(BoardGame game)
            => new BoardGameUpdateViewModel
            {
                Title = game.Title,
                MiniTitle = game.MiniTitle,
                Description = game.Description,
                Essence = game.Essence,
                Tags = game.Tags,
                Price = game.Price,
                ProductCode = game.ProductCode,
            };

        private BoardGame BuildBoardGameDataModelFromUpdate(BoardGameUpdateViewModel gameViewModel)
             => new BoardGame
             {
                 Id = gameViewModel.Id,
                 Title = gameViewModel.Title,
                 MiniTitle = gameViewModel.MiniTitle,
                 Description = gameViewModel.Description,
                 Essence = gameViewModel.Essence,
                 Tags = gameViewModel.Tags,
                 Price = gameViewModel.Price,
                 ProductCode = gameViewModel.ProductCode,
             };

        private BoardGameIndexViewModel BuildBoardGameIndexViewModel(BoardGame game)
            => new BoardGameIndexViewModel
            {
                Id = game.Id,
                Title = game.Title,
            };

        private BoardGameReviewViewModel BuildBoardGameRewievViewModel(BoardGameReview review)
            => new BoardGameReviewViewModel
            {
                Id = review.Id,
                Name = review.Name,
                DateOfCreationInStringFormat = review.DateOfCreation.ToString("dd.MM.yyyy HH:mm"),
                Text = review.Text,
            };


        private BoardGameReview BuildBoardGameRewievDataModelFromCreate(BoardGameCreateReviewViewModel reviewViewModel)
            => new BoardGameReview
            {
                Name = reviewViewModel.Name,
                DateOfCreation = DateTime.Now,
                Text = reviewViewModel.Text,
            };

        private BoardGameReview BuildBoardGameRewievDataModelFromUpdate(BoardGameUpdateReviewViewModel reviewViewModel)
            => new BoardGameReview
            {
                Id = reviewViewModel.Id,
                Name = reviewViewModel.Name,
                Text = reviewViewModel.Text,
            };

        private BoardGameUpdateReviewViewModel BuildBoardGameUpdateRewievDataModel(BoardGameReview review)
            => new BoardGameUpdateReviewViewModel
            {
                Name = review.Name,
                Text = review.Text,
            };
    }
}
