using Microsoft.EntityFrameworkCore;
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

        public List<Game> GetAllWithReviews()
            => _dbContext.Games
            .Include(x => x.Reviews)
            .ToList();

        public void Create(Game game)
        {
            _dbContext.Games.Add(game);

            _dbContext.SaveChanges();
        }

        public void Update(Game game)
        {
            var dbGame = Get(game.Id);

            dbGame.Name = game.Name;
            dbGame.Description = game.Description;
            dbGame.YearOfRelease = game.YearOfRelease;

            _dbContext.SaveChanges();
        }
    }
}
