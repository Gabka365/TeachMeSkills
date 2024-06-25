using BoardGamesRiviewsApi.Data.Models;

namespace BoardGamesRiviewsApi.Data.Repositories
{
    public class BoardGameReviewRepositories
    {
        private ReviewsDbContext _dbContext;

        public BoardGameReviewRepositories(ReviewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BoardGameReview> GetAllForGame(int gameId)
        {
            return _dbContext
                .BoardGameReviews
                .Where(review => review.BoardGameId == gameId)
                .ToList();
        }

        public int Get() => 1;
    }
}
