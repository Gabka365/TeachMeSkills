using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class BookRepositories
    {
        private List<Book> _books = new();
        private int _lastId = 1;
        public void Create(Book book)
        {
            book.Id = _lastId++;
            _books.Add(book);
        }

		public void Delete(int bookId)
		{
			var book = _books.Single(x => x.Id == bookId);
            _books.Remove(book);
		}

		public List<Book> GetAll()
            => _books.ToList();

		public Book Get(int bookId)
		    => _books.Single(x =>x.Id == bookId);

		public void Update(Book book)
		{
			Delete(book.Id);
			Create(book);
		}
	}
}
