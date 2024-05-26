using Microsoft.EntityFrameworkCore;
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

		public List<Movie> GetAllWithReviews()
		{
			return _dbContext.Movies
				.Include(x => x.Reviews)
				.ToList();
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

		public void AddReviewToMovie(int movieId, string comment, int rate)
		{
			var movie = _dbContext.Movies.First(x => x.Id == movieId);
			var review = new MovieReview
			{
				Comment = comment,
				Movie = movie,
				Name = "Movie Review",
				DateOfCreation = DateTime.Now,
				Rate = rate,
			};
			_dbContext.MovieReviews.Add(review);
			_dbContext.SaveChanges();
		}
	}
}
