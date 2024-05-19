using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class MovieRepositories
    {
		private PortalDbContext _dbContext;

		public MovieRepositories(PortalDbContext db)
		{
			_dbContext = db;
		}

        public void Create(Movie movie)
        {
			_dbContext.Movies.Add(movie);
			_dbContext.SaveChanges();
		}

        public List<Movie> GetAll()
        {
            return _dbContext.Movies.ToList();
        }

        public void Delete(int id)
        {
            var movie = _dbContext.Movies.Single(movie => movie.Id == id);
			_dbContext.Movies.Remove(movie);

			_dbContext.SaveChanges();
		}

        public Movie Get(int id)
        {
           return _dbContext.Movies.Single(movie => movie.Id == id);
        }

        public void Update(Movie movie)
        {
            var dbMovie = Get(movie.Id);

			dbMovie.Name = movie.Name;
			dbMovie.Description = movie.Description;
			dbMovie.ReleaseYear = movie.ReleaseYear;
			dbMovie.Director = movie.Director;
			dbMovie.Budget = movie.Budget;
			dbMovie.CountryOfOrigin = movie.CountryOfOrigin;
            
            _dbContext.SaveChanges();
		}
    }
}
