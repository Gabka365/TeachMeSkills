using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class GameRepositories
    {
        // BAD
        // Temporary before we add real DB
        private List<Game> _games = new();
        private int _lastId = 1;

        public Game Get(int id)
            => _games.Single(x => x.Id == id);

        public void Delete(int id)
        {
            var game = _games
                .Single(x => x.Id == id);
            _games.Remove(game);
        }

        //public List<Game> GetAll()
        //{
        //    return _games.ToList();
        //}
        public List<Game> GetAll()
            => _games.ToList();

        public void Create(Game game)
        {
            game.Id = _lastId++;
            _games.Add(game);
        }

        public void Update(Game game)
        {
            Delete(game.Id);
            Create(game);
        }
    }
}
