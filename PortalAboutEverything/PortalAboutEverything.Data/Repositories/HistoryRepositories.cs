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
            => _dbContext.History.Single(x => x.Id == id);

        public List<History> GetAll()
              => _dbContext.History.ToList(); 

        public void Create(History historicalEvent)
        {

            _dbContext.History.Add(historicalEvent);

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var historicalEvents = _dbContext.History
                .Single(x => x.Id == id);
            _dbContext.History.Remove(historicalEvents);
            _dbContext.SaveChanges();
        }
        public void Update (History historicalEvent)
        {
            var dbGame = Get(historicalEvent.Id);

            dbGame.Name = historicalEvent.Name;
            dbGame.Description = historicalEvent.Description;
            dbGame.Date = historicalEvent.Date;

            _dbContext.SaveChanges();
        }

    
    }
}
