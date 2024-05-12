namespace PortalAboutEverything.Models.BookClub
{
    public class BookClubIndexViewModel
    {
        public required string BookAuthor { get; set; }
        public required string BookTitle { get; set; }
        public required List<string> SubjectsOfBook { get; set; }
    }
}
