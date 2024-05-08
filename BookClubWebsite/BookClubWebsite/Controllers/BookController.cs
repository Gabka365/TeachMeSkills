using BookClubWebsite.Controllers.BookEnum;
using BookClubWebsite.Models.Book;
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
				SubjectsOfBook = [Subject.CSharp, Subject.Algorithms, Subject.Database, Subject.AnotherInteresting]
			};
			return View(viewModel);
		}

		public IActionResult BookReview(string bookAuthor, string bookTitle)
		{
			var book = $"{bookAuthor}: {bookTitle}";
			return View(book);
		}
		
	}
}
