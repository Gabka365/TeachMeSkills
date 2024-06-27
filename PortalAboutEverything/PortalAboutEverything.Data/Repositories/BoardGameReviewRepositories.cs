using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class BoardGameReviewRepositories : BaseRepository<BoardGameReview>
    {
        public BoardGameReviewRepositories(PortalDbContext dbContext) : base(dbContext) { }

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
