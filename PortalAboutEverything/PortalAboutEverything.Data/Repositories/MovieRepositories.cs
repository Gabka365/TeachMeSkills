using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
	public class MovieRepositories : BaseRepository<Movie>
	{
		public MovieRepositories(PortalDbContext db) : base(db) { }

		public List<Movie> GetAllWithReviews()
		{
			return _dbContext.Movies
				.Include(x => x.Reviews)
				.ToList();
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

		public List<Movie> GetFavoriteMoviesByUserId(int userId)
			=> _dbSet
			.Where(movie =>
				movie
					.UsersWhoFavoriteTheMovie
					.Any(user => user.Id == userId))
			.ToList();
	}
}
