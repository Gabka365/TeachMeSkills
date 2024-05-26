namespace PortalAboutEverything.Data.Model
{
    public class Book
    {
        public int Id { get; set; }
        public required string BookAuthor { get; set; }
        public required string BookTitle { get; set; }
        public required string SummaryOfBook { get; set; }
        public int YearOfPublication { get; set; }
    }

}
