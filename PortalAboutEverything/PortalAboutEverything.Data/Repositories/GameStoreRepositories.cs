using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories.DataModel;
using PortalAboutEverything.Data.Repositories.RawSql;
using System.Reflection;

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

        public IEnumerable<Top3BuyGamesDataModel> GetTop3BuyGamesGameStore()
        {
            return CustomSqlQuery<Top3BuyGamesDataModel>(SqlQueryManager.GetTop3BuyGamesGameStore).ToList();
        }
    }
}
