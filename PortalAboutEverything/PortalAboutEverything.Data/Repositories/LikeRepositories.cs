using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Data.Repositories
{
    public class LikeRepositories : BaseRepository<Like>
    {
        public LikeRepositories(PortalDbContext db) : base(db) { }

        public void CreateLikeForTraveling(int travelingId, Like like, int userId)
        {
            var traveling = _dbContext.Travelings
                              .Include(t => t.Likes)
                              .FirstOrDefault(t => t.Id == travelingId);
            if (traveling == null)
            {
                return;
            }

            var user = _dbContext.Users
                              .Include(t => t.Likes)
                              .FirstOrDefault(t => t.Id == userId);

            if (like.Id > 0)
            {
                _dbContext.Entry(like).Collection(l => l.Travelings).Load();
            }
            else
            {

                like.Travelings = new List<Traveling>();
            }

            like.Travelings.Add(traveling);
            traveling.Likes.Add(like);
            user.Likes.Add(like);

            _dbContext.Likes.Add(like);
            _dbContext.SaveChanges();
        }

        public Like GetLikeByTravelingPostId(int travelingId, int userId)
            => _dbContext.Travelings
                                   .Where(p => p.Id == travelingId)
                                   .SelectMany(p => p.Likes)
                                   .FirstOrDefault(like => like.Users.Any(user => user.Id == userId));


        public bool CheckLikeUserOnTravelingPost(int userId, int travelingId)
            => _dbContext.Likes
                              .Any(l => l.Users.Any(u => u.Id == userId)
                                 && l.Travelings.Any(t => t.Id == travelingId));
    }
}

