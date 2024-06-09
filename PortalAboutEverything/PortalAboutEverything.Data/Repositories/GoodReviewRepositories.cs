using PortalAboutEverything.Data.Model.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Data.Repositories
{
    public class GoodReviewRepositories : BaseRepository<GoodReview>
    {        
        public GoodReviewRepositories(PortalDbContext db) : base(db) { }       

        public void AddReview(int gameId, string text)
        {
            var good = _dbContext.Goods.FirstOrDefault(x => x.Id == gameId);

            var review = new GoodReview
            {
                Good = good,
                Description = text,
            };
            _dbContext.GoodReviews.Add(review);
            _dbContext.SaveChanges();
        }
    }
}
