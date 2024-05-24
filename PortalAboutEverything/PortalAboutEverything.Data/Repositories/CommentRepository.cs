
using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class CommentRepository : BaseRepository<Comment>
    {
        public CommentRepository(PortalDbContext dbContext) : base(dbContext){}

        public List<Comment> GetWithTravel(int id)
        {
            var test = _dbSet
                    .Where(x => x.Traveling.Id == id)
                    .ToList();
            return test;
        }
    }
           
    
}
