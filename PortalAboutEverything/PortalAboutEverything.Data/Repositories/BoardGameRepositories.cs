using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class BoardGameRepositories : BaseRepository<BoardGame>
    {
        public BoardGameRepositories(PortalDbContext dbContext) : base(dbContext) { }

        public BoardGame GetWithReviews(int id)
            => _dbSet
            .Include(boardGame => boardGame.Reviews)
            .Single(boardGame => boardGame.Id == id);

        public void Update(BoardGame boardGame)
        {
            BoardGame updatedboardGame = Get(boardGame.Id);
            updatedboardGame.Title = boardGame.Title;
            updatedboardGame.MiniTitle = boardGame.MiniTitle;
            updatedboardGame.Description = boardGame.Description;
            updatedboardGame.Essence = boardGame.Essence;
            updatedboardGame.Tags = boardGame.Tags;
            updatedboardGame.Price = boardGame.Price;
            updatedboardGame.ProductCode = boardGame.ProductCode;

            _dbContext.SaveChanges();
        }
    }
}
