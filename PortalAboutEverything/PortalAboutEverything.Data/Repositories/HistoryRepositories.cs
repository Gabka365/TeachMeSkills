using PortalAboutEverything.Data.Migrations;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class HistoryRepositories
    {
        private List<History> _historicalEvents = new();
        private int _lastId = 1;

        public History Get(int id)
            => _historicalEvents.Single(x => x.Id == id);
        public List<History> GetAll()
              => _historicalEvents.ToList(); 
        public void Create(History historicalEvent)
        {
            historicalEvent.Id = _lastId++;
            _historicalEvents.Add(historicalEvent);
        }

        public void Delete(int id)
        {
            var historicalEvents = _historicalEvents
                .Single(x => x.Id == id);
            _historicalEvents.Remove(historicalEvents);
        }
        public void Update (History historicalEvent)
        {
            Delete(historicalEvent.Id);
            Create(historicalEvent);
        }

    
    }
}
