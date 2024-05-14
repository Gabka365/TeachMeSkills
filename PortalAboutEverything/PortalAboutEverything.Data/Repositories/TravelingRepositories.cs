
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class TravelingRepositories
    {
        private List<Traveling> _traveling = new();
        private int _lastId = 1;

        public void Create(Traveling traveling)
        {
            traveling.Id = _lastId++;
            _traveling.Add(traveling);
        }

        public void Delete(int id)
        {
            var traveling = _traveling
               .Single(x => x.Id == id);
            _traveling.Remove(traveling);
        }

        public List<Traveling> GetAll()
        {
           return _traveling.ToList();
        }

        public Traveling Get(int id)
            => _traveling.Single(x => x.Id == id);

        public void Update(Traveling traveling)
        {
            Delete(traveling.Id);
            Create(traveling);
        }
    }
}
