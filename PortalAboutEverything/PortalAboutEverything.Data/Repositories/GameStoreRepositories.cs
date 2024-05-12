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
        private static List<GameStore> _games = new();
        private static int _lastId = 1;

        public void Save(GameStore game)
        {
            game.Id = _lastId++;
            _games.Add(game);
        }
    }
}
