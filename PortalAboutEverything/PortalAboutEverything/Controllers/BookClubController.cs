using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.BookClub;

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateBookViewModel createBookViewModel)
        {
            var book = new Book
            {
                BookAuthor = createBookViewModel.BookAuthor,
                BookTitle = createBookViewModel.BookTitle,
                SummaryOfBook = createBookViewModel.SummaryOfBook,
                YearOfPublication = createBookViewModel.YearOfPublication
            };

            _bookRepositories.Create(book);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _bookRepositories.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var book = _bookRepositories.Get(id);
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
                SummaryOfBook = bookUpdateViewModel.SummaryOfBook,
                YearOfPublication = bookUpdateViewModel.YearOfPublication

            };
            _bookRepositories.Update(book);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult BookReviewPage(BookClubReviewPageViewModel bookClubReviewPageViewModel)
        {
            return View(bookClubReviewPageViewModel);
        }

        public IActionResult BookReviewWritingPage()
        {
            var bookReviewWritingPageViewModel = new BookClubReviewWritingPageViewModel
            {
                BookAuthor = "Стив Макконнелл",
                BookTitle = "Совершенный код",
                SummaryOfBook = "Более 10 лет первое издание этой книги считалось одним из лучших практических руководств" +
                " по программированию. Сейчас эта книга полностью обновлена с учетом современных тенденций и технологий и" +
                " дополнена сотнями новых примеров, иллюстрирующих искусство и науку программирования."
            };

            return View(bookReviewWritingPageViewModel);
        }
        private BookClubIndexViewModel BuildBookClubIndexViewModel(Book book)
             => new BookClubIndexViewModel
             {
                 Id = book.Id,
                 BookAuthor = book.BookAuthor,
                 BookTitle = book.BookTitle,
                 SummaryOfBook = book.SummaryOfBook,
                 YearOfPublication = book.YearOfPublication,
             };

        private BookUpdateViewModel BuildBookUpdateViewModel(Book book)
             => new BookUpdateViewModel
             {
                 Id = book.Id,
                 BookAuthor = book.BookAuthor,
                 BookTitle = book.BookTitle,
                 SummaryOfBook = book.SummaryOfBook,
                 YearOfPublication = book.YearOfPublication
             };

    }
}
