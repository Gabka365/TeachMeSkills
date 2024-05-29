

namespace PortalAboutEverything.Models.BookClub
{
    public class BookClubIndexViewModel
    {
        public int Id { get; set; }
        public required string BookAuthor { get; set; }
        public required string BookTitle { get; set; }
        public required string SummaryOfBook { get; set; }
        public int YearOfPublication { get; set; }

        public List<BookClubReviewViewModel> Review { get; set; }
    }
}
