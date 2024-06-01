using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Model.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Data.Repositories
{
    public class StoreRepositories
    {
        private List<GoodReview> _reviews = new List<GoodReview>();
        private int _lastReviewId = 0;

        private PortalDbContext _dbContext;

        public StoreRepositories(PortalDbContext db)
        {
            _dbContext = db;
        }

        public List<Good> GetAllGoods()
        {
            return _dbContext.Goods.ToList();
        }

        public List<GoodReview> GetAllReviews()
        {
            return _reviews.ToList();
        }

        public void AddGood(Good good)
        {
            _dbContext.Goods.Add(good);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var good = _dbContext.Goods.First(x => x.Id == id);
            _dbContext.Goods.Remove(good);
            _dbContext.SaveChanges();
        }

        public Good GetGoodForUpdate(int id)
        {
            return _dbContext.Goods.Single(x => x.Id == id);
           
        }

        public void UpdateGood(Good good)
        {
            Delete(good.Id);
            AddGood(good);
        }

        public void AddReview(GoodReview review)
        {
            review.Id = _lastReviewId++;
            _reviews.Add(review);
        }
    }
}
