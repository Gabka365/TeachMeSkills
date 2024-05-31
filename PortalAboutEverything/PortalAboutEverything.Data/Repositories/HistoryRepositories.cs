using PortalAboutEverything.Data.Migrations;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    
    public class HistoryRepositories : BaseRepository<HistoryEvent>
    {
        public HistoryRepositories(PortalDbContext db) : base(db) { }

        public List<HistoryEvent> GetFavoriteHistoryEventsByUserId(int userId)
            => _dbSet
            .Where(historyEvent =>
                historyEvent
                    .UserWhoFavoriteTheHistoryEvent
                    .Any(u => u.Id == userId))
            .ToList();
        public void Update (HistoryEvent historicalEvent)
        {
            var dbHistoryEvents= Get(historicalEvent.Id);

            dbHistoryEvents.Name = historicalEvent.Name;
            dbHistoryEvents.Description = historicalEvent.Description;
            dbHistoryEvents.YearOfEvent = historicalEvent.YearOfEvent;

            _dbContext.SaveChanges();
        }

    
    }
}
