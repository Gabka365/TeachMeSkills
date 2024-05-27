using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model.BookClub;

namespace PortalAboutEverything.Data.Repositories
{
    public class BookRepositories
    {
        private PortalDbContext _dbContext;

        public BookRepositories(PortalDbContext db)
        {
            _dbContext = db;
        }

        public List<Book> GetAll()
            => _dbContext.Books.ToList();

        public List<Book> GetAllWithReviews()
            => _dbContext.Books
            .Include(x => x.BookReviews)
            .ToList();

        public Book Get(int id)
            => _dbContext.Books
            .Single(x => x.Id == id);

        public void Create(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = _dbContext.Books
                .Single(x => x.Id == id);
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }

        public void Update(Book book)
        {
            var dbBook = Get(book.Id);

            dbBook.BookTitle = book.BookTitle;
            dbBook.BookAuthor = book.BookAuthor;
            dbBook.YearOfPublication = book.YearOfPublication;
            dbBook.SummaryOfBook = book.SummaryOfBook;

            _dbContext.SaveChanges();
        }

        public void AddBookReviewToBook(int bookId, string name, int bookRating, int printRating, int llustrationsRating, string text)
        {

            var book = _dbContext.Books.First(x => x.Id == bookId);

            var review = new BookReview
            {
                Date = DateTime.Now,
                UserName = name,
                BookRating = bookRating,
                BookPrintRating = printRating,
                BookIllustrationsRating = llustrationsRating,
                Text = text,
                Book = book
            };

            _dbContext.BookReviews.Add(review);
            _dbContext.SaveChanges();
        }
    }
}
