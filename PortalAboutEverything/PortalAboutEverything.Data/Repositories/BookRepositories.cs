using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model.BookClub;

namespace PortalAboutEverything.Data.Repositories
{
    public class BookRepositories : BaseRepository<Book>
    {

        public BookRepositories(PortalDbContext db) : base(db) { }

        public List<Book> GetAllWithReviews()
       => _dbSet
            .Include(x => x.BookReviews)
            .ToList();

        public void Update(Book book)
        {
            var dbBook = Get(book.Id);

            dbBook.BookTitle = book.BookTitle;
            dbBook.BookAuthor = book.BookAuthor;
            dbBook.YearOfPublication = book.YearOfPublication;
            dbBook.SummaryOfBook = book.SummaryOfBook;

            _dbContext.SaveChanges();
        }
    }
}
