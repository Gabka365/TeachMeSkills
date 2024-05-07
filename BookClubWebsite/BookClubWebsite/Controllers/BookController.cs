using BookClubWebsite.Models.Book;
using BookClubWebsite.Models.Book.Enum;
using Microsoft.AspNetCore.Mvc;

namespace BookClubWebsite.Controllers
{
	public class BookController : Controller
	{
		public IActionResult BookPage()
		{
			var viewModel = new BookPageViewModel
			{
				TitleOfBook = "Совершенный код",
				AuthorOfBook = "Стив Макконнелл",
				SubjectsOfBook = new List<Subject> { Subject.CSharp, Subject.Algorithms, Subject.Database }
			};
			return View(viewModel);
		}
	}
}
