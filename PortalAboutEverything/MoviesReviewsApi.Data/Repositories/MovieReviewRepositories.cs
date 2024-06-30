
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

        public List<MovieReview> GetAll()
        {
            return _dbSet.ToList();
        }

        public List<MovieReview> GetAllReviewForOneMovie(int movieId)
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

        public virtual void Delete(MovieReview model)
        {
            _dbSet.Remove(model);
            _dbContext.SaveChanges();
        }

        public void Update(MovieReview movieReview)
        {
            var dbMovie = Get(movieReview.Id);

            dbMovie.Rate = movieReview.Rate;
            dbMovie.Comment = movieReview.Comment;

            _dbContext.SaveChanges();
        }
    }
}
