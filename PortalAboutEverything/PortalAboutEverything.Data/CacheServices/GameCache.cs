using Microsoft.Extensions.DependencyInjection;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;

namespace PortalAboutEverything.Data.CacheServices
{
    public class GameCache
    {
        private List<Game> _games;
        private DateTime _lastUpdate = DateTime.MinValue;

        private IServiceScopeFactory _scopeFactory;

        public GameCache(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        
        public List<Game> GetGames()
        {
            if (_lastUpdate.AddMinutes(30) < DateTime.Now)
            {
                // update 
                using (var scope = _scopeFactory.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<GameRepositories>();
                    _games = repository.GetAll();
                }

                _lastUpdate = DateTime.Now;
            }

            return _games;
        }

        public void ResetCache()
        {
            _lastUpdate = DateTime.MinValue;
        }
    }
}
