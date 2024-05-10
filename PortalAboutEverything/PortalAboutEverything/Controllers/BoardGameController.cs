using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.BoardGame;
using PortalAboutEverything.Models.Game;
using System;

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

        public IActionResult FirstBoardGame()
        {
            List<BoardGameReviewViewModel> reviewViewModels = _reviewRepositories
                .GetAll()
                .Select(BuildBoardGameRewievViewModel)
                .ToList();

            return View(reviewViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BoardGameReviewViewModel boardGameReviewViewModel)
        {
            boardGameReviewViewModel.Date = DateTime.Now;

            BoardGameReview review = new()
            {
                Name = boardGameReviewViewModel.Name,
                Date = boardGameReviewViewModel.Date,
                Text = boardGameReviewViewModel.Text
            };

            _reviewRepositories.Create(review);
            return RedirectToAction("FirstBoardGame");
        }

        private BoardGameReviewViewModel BuildBoardGameRewievViewModel(BoardGameReview review)
            => new BoardGameReviewViewModel
            {
                Id = review.Id,
                Name = review.Name,
                Date = review.Date,
                Text = review.Text,
            };
    }
}
