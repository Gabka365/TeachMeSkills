using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories.DataModel;
using PortalAboutEverything.Data.Repositories.RawSql;

namespace PortalAboutEverything.Data.Repositories
{
	public class MovieRepositories : BaseRepository<Movie>
	{
		public MovieRepositories(PortalDbContext db) : base(db) { }

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

        public IEnumerable<MovieStatisticDataModel> GetMovieStatistic()
        {
            return CustomSqlQuery<MovieStatisticDataModel>(SqlQueryManager.GetMovieStatistic)
                .ToList();
        }

        public bool Exist(int id)
            => _dbSet.Any(x => x.Id == id);

		public List<int> FindAllMovieId()
		{
            return _dbSet.Select(movie => movie.Id).ToList();
        }

		public string GetMovieName(int id)
		{
			var movieName = Get(id).Name;
			return movieName;
		}
    }
}
