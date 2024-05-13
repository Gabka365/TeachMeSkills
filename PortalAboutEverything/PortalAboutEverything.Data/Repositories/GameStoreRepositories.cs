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
        private List<GameStore> _games = new();
        private int _lastId = 1;

        public void Delete(int id)
        {
            var game =
                _games.Single(x => x.Id == id);
            _games.Remove(game);
        }

        public GameStore Get(int id)
            => _games.Single(x => x.Id == id);

        public List<GameStore> GetAll()
            => _games.ToList();

        public void Create(GameStore game)
        {
            game.Id = _lastId++;
            _games.Add(game);
        }

        public void Update(GameStore game)
        {
            Delete(game.Id);
            Create(game);
        }
    }
}
