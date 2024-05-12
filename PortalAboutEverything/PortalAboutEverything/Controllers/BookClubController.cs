using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.BookClub;
using System.Globalization;

namespace PortalAboutEverything.Controllers
{
    public class BookClubController : Controller
    {

        private BookRepositories _bookRepositories;

        public BookClubController(BookRepositories bookRepositories)
        {
            _bookRepositories = bookRepositories;   
        }
        public IActionResult Index()
        {
            var books = _bookRepositories
                .GetAll()
                .Select(BuildBookClubIndexViewModel)
                .ToList();
           
            return View(books);
        }

        public IActionResult BookReviewWritingPage()
        {
            var bookReviewWritingPageViewModel = new BookClubReviewWritingPageViewModel
            {
                BookAuthor = "Стив Макконнелл",
                BookTitle = "Совершенный код",
                SubjectsOfBook = [Subject.CSharp, Subject.Algorithms, Subject.Database],
                SummaryOfBook = "Более 10 лет первое издание этой книги считалось одним из лучших практических руководств" +
                " по программированию. Сейчас эта книга полностью обновлена с учетом современных тенденций и технологий и" +
                " дополнена сотнями новых примеров, иллюстрирующих искусство и науку программирования."
            };

            return View(bookReviewWritingPageViewModel);
        }

        [HttpPost]
        public IActionResult BookReviewPage(BookClubReviewPageViewModel bookClubReviewPageViewModel)
        {
            return View(bookClubReviewPageViewModel);
        }

        [HttpGet]
        public IActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBook(CreateBookViewModel createBookViewModel)
        {
            var book = new Book
            {
                BookAuthor = createBookViewModel.BookAuthor,
                BookTitle = createBookViewModel.BookTitle,
                SubjectsOfBook = createBookViewModel.SubjectsOfBook,
                SummaryOfBook = createBookViewModel.SummaryOfBook,
                YearOfPublication = createBookViewModel.YearOfPublication
            };

            _bookRepositories.Create(book);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int bookId) {
            _bookRepositories.Delete(bookId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int bookId) {
            var book = _bookRepositories.Get(bookId);
            var viewModel = BuildBookUpdateViewModel(book);
			return View(viewModel);

		}

		[HttpPost]
		public IActionResult Update(BookUpdateViewModel bookUpdateViewModel)
		{
            var book = new Book
            {
                Id = bookUpdateViewModel.Id,
                BookAuthor = bookUpdateViewModel.BookAuthor,
                BookTitle = bookUpdateViewModel.BookTitle,
                SubjectsOfBook = bookUpdateViewModel.SubjectsOfBook,
                SummaryOfBook = bookUpdateViewModel.SummaryOfBook,
                YearOfPublication = bookUpdateViewModel.YearOfPublication

            };
            _bookRepositories.Update(book);
			return RedirectToAction("Index");

		}

		private BookClubIndexViewModel BuildBookClubIndexViewModel(Book book)
             => new BookClubIndexViewModel
             {
                 Id = book.Id,
                 BookAuthor = book.BookAuthor,
                 BookTitle = book.BookTitle,
                 SubjectsOfBook = book.SubjectsOfBook,
                 SummaryOfBook = book.SummaryOfBook,
                 YearOfPublication = book.YearOfPublication
             };

        private BookUpdateViewModel BuildBookUpdateViewModel(Book book)
             => new BookUpdateViewModel
             {
                 Id = book.Id,
                 BookAuthor = book.BookAuthor,
                 BookTitle = book.BookTitle,
                 SubjectsOfBook = book.SubjectsOfBook,
                 SummaryOfBook = book.SummaryOfBook,
                 YearOfPublication = book.YearOfPublication
             };

    }
}
