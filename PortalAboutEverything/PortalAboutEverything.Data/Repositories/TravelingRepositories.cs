

using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class TravelingRepositories
    {
        private List<Traveling> _travelings = new();

        private int _lastId = 1;

        public List<Traveling> GetAll()
            => _travelings.ToList();

        public Traveling Get(int id)
            => _travelings.Single(x => x.Id == id);

        
        public void Create(Traveling traveling)
        {
            traveling.Id = _lastId++;
            _travelings.Add(traveling);
        }
    }
}
