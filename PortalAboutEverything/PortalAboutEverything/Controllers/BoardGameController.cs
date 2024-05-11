using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.BoardGame;
using System;

namespace PortalAboutEverything.Controllers
{
    public class BoardGameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FirstBoardGame()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FirstBoardGame(BoardGameReviewViewModel boardGameReviewViewModel)
        {
            boardGameReviewViewModel.Date = DateTime.Now;

            return View(boardGameReviewViewModel);
        }

        public IActionResult ReviewForm()
        {
            return View();
        }
    }
}
