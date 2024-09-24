using BookClubWebsite.Controllers.BookEnum;

namespace BookClubWebsite.Models.Book
{
    public class BookPageViewModel
	{
        public string TitleOfBook { get; set; }
        public string AuthorOfBook { get; set; }
        public List<Subject> SubjectsOfBook { get; set; }
    }
}
