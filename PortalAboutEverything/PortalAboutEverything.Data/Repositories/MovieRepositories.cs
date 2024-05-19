using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class MovieRepositories
    {
        private List<Movie> _movies = new ();
        private int _lastId = 1;

        public void Create(Movie movie)
        {
            movie.Id = _lastId++;
            _movies.Add(movie);
        }

        public List<Movie> GetAll()
        {
            return _movies;
        }

        public void Delete(int id)
        {
            var movie = _movies.FirstOrDefault(movie => movie.Id == id);
            _movies.Remove(movie);
        }

        public Movie Get(int id)
        {
           return _movies.Single(movie => movie.Id == id);
        }

        public void Update(Movie movie)
        {
            Delete(movie.Id);
            Create(movie);
        }
    }
}
