using System.Collections;

namespace PortalAboutEverything.Models.BookClub
{
	public class BookClubReviewWritingPageViewModel
	{
		public required string BookAuthor { get; set; }
		public required string BookTitle { get; set; }
		public required List<string> SubjectsOfBook { get; set; }
		public List<int> BookRates { get; set; } = Enumerable.Range(1, 10).ToList();
		public List<int> BookPrintRates { get; set; } = Enumerable.Range(1, 10).ToList();
		public List<int> BookIllustrationsRates { get; set; } = Enumerable.Range(1, 10).ToList();

	}
}
