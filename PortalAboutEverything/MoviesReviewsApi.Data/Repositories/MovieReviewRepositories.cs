
using Microsoft.EntityFrameworkCore;
using MoviesReviewsApi.Data.Model;

namespace MoviesReviewsApi.Data.Repositories
{
    public class MovieReviewRepositories 
    {
        protected MoviesReviewsDbContext _dbContext;
        protected DbSet<MovieReview> _dbSet;

        public MovieReviewRepositories(MoviesReviewsDbContext dbContext) 
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<MovieReview>();
        }

        public List<MovieReview> FindReviewsByMovieId(int movieId)
        {
            return _dbSet.Where(movieReview => movieReview.MovieId == movieId).ToList();
        }

        public MovieReview Get(int id)
        {
            return _dbSet.FirstOrDefault(dbModel => dbModel.Id == id);
        }

        public MovieReview Create(MovieReview model)
        {
            _dbSet.Add(model);

            _dbContext.SaveChanges();

            return model;
        }

        public void Delete(int id)
        {
            var model = Get(id);

            if (model is null)
            {
                throw new KeyNotFoundException();
            }

            Delete(model);
        }

        public void Delete(MovieReview model)
        {
            _dbSet.Remove(model);
            _dbContext.SaveChanges();
        }

        public void DeleteAllReviewsByMovieId(int movieId)
        {
            var reviews = FindReviewsByMovieId(movieId);
            foreach (var review in reviews) 
            {
				Delete(review);
			}
		}

        public int GetAmountOfReviewsByMovie(int movieId)
        {
            var reviews = FindReviewsByMovieId(movieId);
            return reviews.Count();
        }

        public void Update(MovieReview movieReview)
        {
            var review = Get(movieReview.Id);

            review.Rate = movieReview.Rate;
            review.Comment = movieReview.Comment;

            _dbContext.SaveChanges();
        }

        public int GetAverageRateOfMovie(int movieId)
        {
            var averageRate = 0;
            var reviewList = FindReviewsByMovieId(movieId);

            if (reviewList.Count() != 0)
            {
                averageRate = (int)reviewList.Select(rev => rev.Rate).Average();
            }
               
            return averageRate;
        }
    }
}
