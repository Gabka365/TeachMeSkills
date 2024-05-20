using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class BoardGameRepositories
    {
        private PortalDbContext _dbContext;

        public BoardGameRepositories(PortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BoardGame> GetAll() => _dbContext.BoardGames.ToList();

        public void Create(BoardGame boardGame)
        {
            _dbContext.BoardGames.Add(boardGame);

            _dbContext.SaveChanges();
        }

        public BoardGame Get(int id)
            => _dbContext.BoardGames.Single(boardGame => boardGame.Id == id);

        public void Delete(int id)
        {
            BoardGame boardGame = _dbContext.BoardGames.Single(boardGame => boardGame.Id == id);
            _dbContext.BoardGames.Remove(boardGame);

            _dbContext.SaveChanges();
        }

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
