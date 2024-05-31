using PortalAboutEverything.Data.Model.BookClub;

namespace PortalAboutEverything.Data.Repositories
{
    public class BookReviewRepositories : BaseRepository<BookReview>
    {
        public BookReviewRepositories(PortalDbContext db) : base(db) { }
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
