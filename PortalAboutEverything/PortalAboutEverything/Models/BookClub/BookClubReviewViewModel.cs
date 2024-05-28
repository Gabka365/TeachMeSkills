using Microsoft.Extensions.Primitives;

namespace PortalAboutEverything.Models.BookClub
{
    public class BookClubReviewViewModel
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int BookRating { get; set; }
        public int PrintRating { get; set; }
        public int IlustrationsRating { get; set; }
        public string Text { get; set; }
    }
}
