using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class BoardGameReviewRepositories : BaseRepository<BoardGameReview>
    {
        public BoardGameReviewRepositories(PortalDbContext dbContext) : base(dbContext) { }

        public void Create(BoardGameReview review, int boardGameId)
        {
            BoardGame boardGame = _dbContext.BoardGames.First(game => game.Id == boardGameId);
            review.BoardGame = boardGame;

            _dbSet.Add(review);
            _dbContext.SaveChanges();
        }

        public BoardGameReview GetWithBoardGame(int id)
            => _dbSet
            .Include(review => review.BoardGame)
            .Single(review => review.Id == id);

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
