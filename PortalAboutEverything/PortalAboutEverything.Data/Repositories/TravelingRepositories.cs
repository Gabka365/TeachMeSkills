
using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories.DataModel;
using PortalAboutEverything.Data.Repositories.RawSql;

namespace PortalAboutEverything.Data.Repositories
{
    public class TravelingRepositories : BaseRepository<Traveling>
    {
        public TravelingRepositories(PortalDbContext dbContext) : base(dbContext) { }
        
        public void Update(Traveling traveling)
        {
            var dbTraveling = Get(traveling.Id);
            dbTraveling.Name = traveling.Name;
            dbTraveling.Desc = traveling.Desc;
            _dbContext.SaveChanges();
        }
        public override List<Traveling> GetAll()
        {
            return _dbSet.Include(x => x.User).ToList();
        }
        public TopTravelingByCommentsDataModel GetTopTreveling()
        {   
            var topTraveling = CustomSqlQuery<TopTravelingByCommentsDataModel>(SqlQueryManager.TopTravelingByComments).First();
            return topTraveling;

        }
        public int CountLike(int travelingId)
        {
            if (GetLikesForTraveling(travelingId) != null)
            {
                return GetLikesForTraveling(travelingId).Count;
            }
            else
            {
                return 0;
            }
           
        }
        public List<Like> GetLikesForTraveling(int travelingId)
        {
            var traveling = _dbContext.Travelings
                                      .Include(t => t.Likes)
                                      .FirstOrDefault(t => t.Id == travelingId);

            if (traveling != null)
            {
                return traveling.Likes.ToList();
            }
            else
            {
                return null;
               
            }
        }
    }
}

