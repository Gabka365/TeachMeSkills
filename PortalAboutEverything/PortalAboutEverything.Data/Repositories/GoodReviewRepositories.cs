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

        public void AddReview(int goodId, string text, string userName)
        {
            var good = _dbContext.Goods.FirstOrDefault(x => x.Id == goodId);

            var review = new GoodReview
            {
                Good = good,
                Text = text,
                UserWhoLeavedAReview = userName
            };
            _dbContext.GoodReviews.Add(review);
            _dbContext.SaveChanges();
        }
    }
}
