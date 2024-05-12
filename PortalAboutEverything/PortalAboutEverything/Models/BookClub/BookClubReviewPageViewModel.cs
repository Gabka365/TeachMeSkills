using Microsoft.Extensions.Primitives;

namespace PortalAboutEverything.Models.BookClub
{
    public class BookClubReviewPageViewModel
    {
        public required string BookAuthor { get; set; }
        public required string BookTitle { get; set; }
        public required int BookRating { get; set; }
        public required int BookPrintRating { get; set; }
        public required int BookIllustrationsRating { get; set; }
        public required string TextReview { get; set; }
       
    }
}
