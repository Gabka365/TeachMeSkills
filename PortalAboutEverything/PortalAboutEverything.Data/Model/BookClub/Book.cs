namespace PortalAboutEverything.Data.Model.BookClub
{
	public class Book: BaseModel
    {
		public required string BookAuthor { get; set; }
		public required string BookTitle { get; set; }
		public required string SummaryOfBook { get; set; }
		public int YearOfPublication { get; set; }
		public virtual List<BookReview>? BookReviews { get; set; }
        public virtual List<User> UsersWhoAddBookToFavorites { get; set; }
    }

}
