using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.BoardGame;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            BoardGameReview review = BuildBoardGameRewievDataModelFromCreate(boardGameReviewViewModel);

            _reviewRepositories.Create(review);
            return RedirectToAction("FirstBoardGame");
        }

        public IActionResult FirstBoardGame()
        {
            List<BoardGameReviewViewModel> reviewViewModel = _reviewRepositories
                .GetAll()
                .Select(BuildBoardGameRewievViewModel)
                .ToList();

            return View(reviewViewModel);
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
