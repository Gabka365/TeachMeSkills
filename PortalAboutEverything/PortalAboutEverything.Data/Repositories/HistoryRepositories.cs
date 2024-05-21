using PortalAboutEverything.Data.Migrations;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    
    public class HistoryRepositories
    {
        private PortalDbContext _dbContext;

        public HistoryRepositories(PortalDbContext db)
        {
            _dbContext = db;
        }    

        public History Get(int id)
            => _dbContext.HistoryEvents.Single(x => x.Id == id);

        public List<History> GetAll()
              => _dbContext.HistoryEvents.ToList(); 

        public void Create(History historicalEvent)
        {

            _dbContext.HistoryEvents.Add(historicalEvent);

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var historicalEvents = _dbContext.HistoryEvents
                .Single(x => x.Id == id);
            _dbContext.HistoryEvents.Remove(historicalEvents);
            _dbContext.SaveChanges();
        }
        public void Update (History historicalEvent)
        {
            var dbHistoryEvents= Get(historicalEvent.Id);

            dbHistoryEvents.Name = historicalEvent.Name;
            dbHistoryEvents.Description = historicalEvent.Description;
            dbHistoryEvents.YearOfEvent = historicalEvent.YearOfEvent;

            _dbContext.SaveChanges();
        }

    
    }
}
