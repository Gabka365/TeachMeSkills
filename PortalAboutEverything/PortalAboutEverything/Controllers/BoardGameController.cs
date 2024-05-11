using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.BoardGame;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;

namespace PortalAboutEverything.Controllers
{
    public class BoardGameController : Controller
    {
        private BoardGameReviewRepositories _reviewRepositories;

        public BoardGameController(BoardGameReviewRepositories reviewRepositories)
        {
            _reviewRepositories = reviewRepositories;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BoardGameCreateReviewViewModel boardGameReviewViewModel)
        {
            boardGameReviewViewModel.Date = DateTime.Now;

            BoardGameReview review = BuildBoardGameRewievDataModelFromCreate(boardGameReviewViewModel);

            _reviewRepositories.Create(review);
            return RedirectToAction("FirstBoardGame");
        }

        public IActionResult FirstBoardGame()
        {
            List<BoardGameReviewViewModel> reviewViewModels = _reviewRepositories
                .GetAll()
                .Select(BuildBoardGameRewievViewModel)
                .ToList();

            return View(reviewViewModels);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            BoardGameReview reviewForUpdate = _reviewRepositories.Get(id);
            BoardGameUpdateReviewViewModel viewModel = BuildBoardGameUpdateRewievDataModel(reviewForUpdate);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(BoardGameUpdateReviewViewModel boardGameReviewViewModel)
        {
            BoardGameReview updatedReview = BuildBoardGameRewievDataModelFromUpdate(boardGameReviewViewModel);
            _reviewRepositories.Update(updatedReview);

            return RedirectToAction("FirstBoardGame");
        }
       
        public IActionResult Delete(int id)
        {
            _reviewRepositories.Delete(id);

            return RedirectToAction("FirstBoardGame");
        }

        private BoardGameReviewViewModel BuildBoardGameRewievViewModel(BoardGameReview review)
            => new BoardGameReviewViewModel
            {
                Id = review.Id,
                Name = review.Name,
                DateInStringFormat = review.Date.ToString("dd.MM.yyyy HH:mm"),
                Text = review.Text,
            };

        private BoardGameReview BuildBoardGameRewievDataModelFromCreate(BoardGameCreateReviewViewModel reviewViewModel)
            => new BoardGameReview
            {
                Name = reviewViewModel.Name,
                Date = reviewViewModel.Date,
                Text = reviewViewModel.Text,
            };

        private BoardGameReview BuildBoardGameRewievDataModelFromUpdate(BoardGameUpdateReviewViewModel reviewViewModel)
            => new BoardGameReview
            {
                Id = reviewViewModel.Id,
                Name = reviewViewModel.Name,
                Date = reviewViewModel.Date,
                Text = reviewViewModel.Text,
            };

        private BoardGameUpdateReviewViewModel BuildBoardGameUpdateRewievDataModel(BoardGameReview review)
            => new BoardGameUpdateReviewViewModel
            {
                Name = review.Name,
                Date = review.Date,
                Text = review.Text,
            };
    }
}
