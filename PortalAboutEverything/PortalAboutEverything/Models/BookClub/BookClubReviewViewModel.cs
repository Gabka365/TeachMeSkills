using Microsoft.Extensions.Primitives;

namespace PortalAboutEverything.Models.BookClub
{
	public class BookClubReviewViewModel
	{
        public  int bookId { get; set; }
        public string Name { get; set; }
		public DateTime Date { get; set; }
		public int BookRating { get; set; }
		public int PrintRating { get; set; }
		public int llustrationsRating { get; set; }
		public string Text { get; set; }

	}
}
