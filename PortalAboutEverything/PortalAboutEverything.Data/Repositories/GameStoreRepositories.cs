using PortalAboutEverything.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Data.Repositories
{
    public class GameStoreRepositories
    {
        private PortalDbContext _dbContex;

        public GameStoreRepositories(PortalDbContext db)
        {
            _dbContex = db;
        }

        public void Delete(int id)
        {
            var game =
                _dbContex.GameStores.Single(x => x.Id == id);
            _dbContex.GameStores.Remove(game);

            _dbContex.SaveChanges();
        }

        public GameStore Get(int id)
            => _dbContex.GameStores.Single(x => x.Id == id);

        public List<GameStore> GetAll()
            => _dbContex.GameStores.ToList();

        public void Create(GameStore game)
        {
            _dbContex.GameStores.Add(game);

            _dbContex.SaveChanges();
        }

        public void Update(GameStore game)
        {
            Delete(game.Id);
            Create(game);
        }
    }
}
