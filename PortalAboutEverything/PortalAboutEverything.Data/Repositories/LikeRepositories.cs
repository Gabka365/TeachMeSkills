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

            var user = _dbContext.Users
                              .Include(t => t.Likes)
                              .FirstOrDefault(t => t.Id == userId);
            if (traveling != null)
            {

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
        }
               
        public Like GetLikeByTravelingPostId(int postId)//TODO need a test
        {
            
            var like = _dbContext.Likes
                .Where(l => l.Travelings.Any(pl => pl.Id == postId))
                .FirstOrDefault(); 

            return like;
        }

    }
}

