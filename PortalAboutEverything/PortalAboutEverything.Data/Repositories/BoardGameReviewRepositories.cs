using Microsoft.EntityFrameworkCore;
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

        public void Create(BoardGameReview review, int boardGameId)
        {
            BoardGame boardGame = _dbContext.BoardGames.First(game => game.Id == boardGameId);
            review.BoardGame = boardGame;
            _dbContext.BoardGameReviews.Add(review);

            _dbContext.SaveChanges();
        }

        public BoardGameReview Get(int id)
            => _dbContext.BoardGameReviews.Single(review => review.Id == id);

        public BoardGameReview GetWithBoardGame(int id)
            => _dbContext.BoardGameReviews
            .Include(review => review.BoardGame)
            .Single(review => review.Id == id);

        public void Delete(int id)
        {
            BoardGameReview review = _dbContext.BoardGameReviews.Single(review => review.Id == id);
            _dbContext.BoardGameReviews.Remove(review);

            _dbContext.SaveChanges();
        }

        public void Update(BoardGameReview review, int boardGameId)
        {
            BoardGame boardGame = _dbContext.BoardGames.First(game => game.Id == boardGameId);
            review.BoardGame = boardGame;

            BoardGameReview updatedReview = Get(review.Id);
            updatedReview.Name = review.Name;
            updatedReview.Text = review.Text;

            _dbContext.SaveChanges();
        }

        public void AddReviewToGame(int gameId, string text)
        {
            var game = _dbContext.Games.First(x => x.Id == gameId);

            var review = new BoardGameReview
            {
                Text = text,
                Game = game,
                DateOfCreation = DateTime.Now,
                Name = "Game Review"
            };

            _dbContext.BoardGameReviews.Add(review);
            _dbContext.SaveChanges();
        }
    }
}
