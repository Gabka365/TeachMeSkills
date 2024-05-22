
using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class TravelingRepositories : RepositoriesTest<Traveling>
    {
        public TravelingRepositories(PortalDbContext dbContext) : base(dbContext) { }

        public override void Update(Traveling traveling)
        {
            var dbTraveling = Get(traveling.Id);

            dbTraveling.Name = traveling.Name;
            dbTraveling.Desc = traveling.Desc;
            _dbContext.SaveChanges();
        }
    }

}

