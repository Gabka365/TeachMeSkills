using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class GameStoreRepositories : BaseRepository<GameStore>
    {
        public GameStoreRepositories(PortalDbContext db) : base(db) { }

        public List<GameStore> GetGamesByUserId(int userId)
            => _dbSet
            .Where(game =>
                game
                    .UserTheGame
                    .Any(u => u.Id == userId))
            .ToList();

        public void Update(GameStore game)
        {
            var dbGame = Get(game.Id);

            dbGame.GameName = game.GameName;
            dbGame.Developer = game.Developer;
            dbGame.YearOfRelease = game.YearOfRelease;

            _dbContext.SaveChanges();
        }
    }
}
