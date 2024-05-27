

namespace PortalAboutEverything.Models.BookClub
{
    public class BookClubReviewWritingViewModel
    {
        public int Id { get; set; }
        public string BookAuthor { get; set; }
        public string BookTitle { get; set; }
        public  string SummaryOfBook { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public List<int> BookRates { get; set; } = Enumerable.Range(1, 10).ToList();
        public List<int> BookPrintRates { get; set; } = Enumerable.Range(1, 10).ToList();
        public List<int> BookIllustrationsRates { get; set; } = Enumerable.Range(1, 10).ToList();
        public string Text { get; set; }

    }
}
