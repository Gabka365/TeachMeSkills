using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class BookRepositories
    {
        private List<Book> _books = new();
        private int _lastId = 1;

        public List<Book> GetAll()
            => _books.ToList();

        public Book Get(int id)
            => _books.
            Single(x => x.Id == id);

        public void Create(Book book)
        {
            book.Id = _lastId++;
            _books.Add(book);
        }

        public void Delete(int id)
        {
            var book = _books
                .Single(x => x.Id == id);
            _books.Remove(book);
        }

        public void Update(Book book)
        {
            Delete(book.Id);
            Create(book);
        }
    }
}
