
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class TravelingRepositories
    {
        private PortalDbContext _dbContext;
     
        public TravelingRepositories(PortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Traveling traveling)
        {
            _dbContext.Add(traveling);

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var traveling = _dbContext.Travelings
               .Single(x => x.Id == id);
            _dbContext.Remove(traveling);

            _dbContext.SaveChanges();
        }

        public List<Traveling> GetAll()
        {
            return _dbContext.Travelings.ToList();
        }

        public Traveling Get(int id)
            => _dbContext.Travelings.Single(x => x.Id == id);

        public void Update(Traveling traveling)
        {
            var dbTraveling = Get(traveling.Id);

            dbTraveling.Name = traveling.Name;
            dbTraveling.Desc = traveling.Desc;
            _dbContext.SaveChanges();
        }
    }
}

