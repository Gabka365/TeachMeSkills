using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class GameRepositories
    {
        private PortalDbContext _dbContext;

        public GameRepositories(PortalDbContext db)
        {
            _dbContext = db;
        }

        public Game Get(int id)
            => _dbContext.Games.Single(x => x.Id == id);

        public void Delete(int id)
        {
            var game = _dbContext.Games
                .Single(x => x.Id == id);
            _dbContext.Games.Remove(game);
            
            _dbContext.SaveChanges();
        }

        //public List<Game> GetAll()
        //{
        //    return _games.ToList();
        //}
        public List<Game> GetAll()
            => _dbContext.Games.ToList();

        public void Create(Game game)
        {
            _dbContext.Games.Add(game);

            _dbContext.SaveChanges();
        }

        public void Update(Game game)
        {
            Delete(game.Id);
            Create(game);
        }
    }
}
