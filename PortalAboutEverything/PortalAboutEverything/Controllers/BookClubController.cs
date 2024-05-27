using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model.BookClub;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.BookClub;

namespace PortalAboutEverything.Controllers
{
    public class BookClubController : Controller
    {

        private BookRepositories _bookRepositories;
        private BookReviewRepositories _bookReviewRepositories;
        public BookClubController(BookRepositories bookRepositories, BookReviewRepositories bookReviewRepositories)
        {
            _bookRepositories = bookRepositories;
            _bookReviewRepositories = bookReviewRepositories;
        }

        public IActionResult Index()
        {
            var books = _bookRepositories
                .GetAllWithReviews()
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

        [HttpGet]
        public IActionResult AddBookReview(int id)
        {
            var book = _bookRepositories.Get(id);
            var bookClubReviewWritingViewModel = new BookClubReviewWritingViewModel
            {
                BookId = id,
                BookAuthor = book.BookAuthor,
                BookTitle = book.BookTitle,
                SummaryOfBook = book.SummaryOfBook,
            };

            return View(bookClubReviewWritingViewModel);
        }

        [HttpPost]
        public IActionResult AddBookReview(BookClubReviewViewModel viewModel)
        {
            _bookReviewRepositories.AddBookReviewToBook(viewModel.BookId, viewModel.Name,
                viewModel.BookRating, viewModel.PrintRating, viewModel.IlustrationsRating, viewModel.Text);

            return RedirectToAction("Index");
        }

        private BookClubIndexViewModel BuildBookClubIndexViewModel(Book book)
             => new BookClubIndexViewModel
             {
                 Id = book.Id,
                 BookAuthor = book.BookAuthor,
                 BookTitle = book.BookTitle,
                 SummaryOfBook = book.SummaryOfBook,
                 YearOfPublication = book.YearOfPublication,
                 Review = book
                        .BookReviews
                        .Select(BuildBookClubReviewViewModel)
                        .ToList()
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

        private BookClubReviewViewModel BuildBookClubReviewViewModel(BookReview review)
            => new BookClubReviewViewModel
            {
                Name = review.UserName,
                Date = review.Date,
                BookRating = review.BookRating,
                PrintRating = review.BookPrintRating,
                IlustrationsRating = review.BookIllustrationsRating,
                Text = review.Text
            };


    }
}
