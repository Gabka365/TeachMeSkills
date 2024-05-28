
namespace PortalAboutEverything.Models.BookClub
{
    public class BookUpdateViewModel
    {
        public int Id { get; set; }
        public required string BookAuthor { get; set; }
        public required string BookTitle { get; set; }
        public required string SummaryOfBook { get; set; }
        public int YearOfPublication { get; set; }
    }
}
