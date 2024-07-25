using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model.Alerts;
using PortalAboutEverything.Data.Repositories.Interfaces;

namespace PortalAboutEverything.Data.Repositories
{
    public class AlertRepository : BaseRepository<Alert>, IAlertRepository
    {
        public AlertRepository(PortalDbContext dbContext) : base(dbContext)
        {
        }

        public List<Alert> GetAlertsForUser(int userId)
        {
            return _dbSet
                .Where(x => !x.UsersWhoAlreadySawIt
                    .Any(alertUser => alertUser.User.Id == userId))
                .Where(x => x.EndDate == null || x.EndDate > DateTime.Now)
                .ToList();
        }

        public void UserWasNottified(int userId, int alertId)
        {
            var alert = _dbSet.First(x => x.Id == alertId);
            var user = _dbContext
                .Users
                .Include(x => x.AlertsWhichISaw)
                .First(x => x.Id == userId);

            var alertUser = new AlertUser()
            {
                User = user,
                Alert = alert,
                WhenUserSawAlert = DateTime.Now
            };

            user.AlertsWhichISaw.Add(alertUser);
            _dbContext.SaveChanges();
        }
    }
}
