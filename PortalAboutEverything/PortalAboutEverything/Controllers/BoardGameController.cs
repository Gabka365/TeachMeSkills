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
            return View();
        }

        [HttpGet]
        public IActionResult CreateReview()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateReview(BoardGameCreateReviewViewModel boardGameReviewViewModel)
        {
            BoardGameReview review = BuildBoardGameRewievDataModelFromCreate(boardGameReviewViewModel);

            _reviewRepositories.Create(review);
            return RedirectToAction("BoardGame");
        }

        public IActionResult BoardGame(int id)
        {
            //BoardGame gameViewModel = _gameRepositories.Get(id);
            //BoardGameViewModel viewModel = BuildBoardGameViewModel(gameViewModel);

            List<BoardGameReviewViewModel> reviewViewModel = _reviewRepositories
                .GetAll()
                .Select(BuildBoardGameRewievViewModel)
                .ToList();

            return View(reviewViewModel);
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
            foreach (var review in game.Reviews)
            {
                reviewViewModels.Add(BuildBoardGameRewievViewModel(review));
            }

            return new BoardGameViewModel
            {
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
