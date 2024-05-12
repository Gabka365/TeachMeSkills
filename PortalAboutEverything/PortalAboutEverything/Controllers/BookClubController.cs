using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.BookClub;
using System.Globalization;

namespace PortalAboutEverything.Controllers
{
    public class BookClubController : Controller
    {
        public IActionResult Index()
        {
            var bookViewModel = new BookClubIndexViewModel
            {
                BookAuthor = "Стив Макконнелл",
                BookTitle = "Совершенный код",
                SubjectsOfBook = ["C#", "Алгоритмы", "БД"]
            };
            return View(bookViewModel);
        }

        public IActionResult BookReviewWritingPage()
        {
            var bookReviewWritingPageViewModel = new BookClubReviewWritingPageViewModel
            {
                BookAuthor = "Стив Макконнелл",
                BookTitle = "Совершенный код",
                SubjectsOfBook = ["C#", "Алгоритмы", "БД"]
            };

            return View(bookReviewWritingPageViewModel);
        }

        [HttpPost]
		public IActionResult BookReviewPage(BookClubReviewPageViewModel bookClubReviewPageViewModel)
		{
			return View(bookClubReviewPageViewModel);
		}
	}
}
