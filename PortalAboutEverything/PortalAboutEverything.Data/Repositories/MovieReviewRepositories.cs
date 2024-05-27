using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
	public class MovieReviewRepositories
	{
		private PortalDbContext _dbContext;

		public MovieReviewRepositories(PortalDbContext db)
		{
			_dbContext = db;
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
