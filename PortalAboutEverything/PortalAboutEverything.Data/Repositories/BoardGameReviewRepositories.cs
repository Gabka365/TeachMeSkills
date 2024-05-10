using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class BoardGameReviewRepositories
    {
        private List<BoardGameReview> _boardGameReviews = new();
        private int _lastId = 1;

        public List<BoardGameReview> GetAll() => _boardGameReviews;

        public void Create(BoardGameReview review)
        {
            review.Id = _lastId++;
            _boardGameReviews.Add(review);
        }
        
        public BoardGameReview Get(int id) 
            => _boardGameReviews.Single(review => review.Id == id);

        public void Delete(int id)
        {
            BoardGameReview review = _boardGameReviews.Single(review => review.Id == id);
            _boardGameReviews.Remove(review);
        }

        public void Update(BoardGameReview review)
        {
            Delete(review.Id);
            Create(review);
        }
    }
}
