using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class BoardGameReviewRepositories
    {
        private PortalDbContext _dbContext;

        public BoardGameReviewRepositories(PortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BoardGameReview> GetAll() => _dbContext.BoardGameReviews.ToList();

        public void Create(BoardGameReview review)
        {
            _dbContext.BoardGameReviews.Add(review);

            _dbContext.SaveChanges();
        }
        
        public BoardGameReview Get(int id) 
            => _dbContext.BoardGameReviews.Single(review => review.Id == id);

        public void Delete(int id)
        {
            BoardGameReview review = _dbContext.BoardGameReviews.Single(review => review.Id == id);
            _dbContext.BoardGameReviews.Remove(review);

            _dbContext.SaveChanges();
        }

        public void Update(BoardGameReview review)
        {
            BoardGameReview updatedReview = Get(review.Id);
            updatedReview.Name = review.Name;
            updatedReview.Text = review.Text;

            _dbContext.SaveChanges();
        }
    }
}
