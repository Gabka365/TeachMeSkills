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
        private List<Good> _goods = new List<Good>();
        private List<GoodReview> _reviews = new List<GoodReview>();
        private int _lastGoodId = 0;
        private int _lastReviewId = 0;

        public List<Good> GetAllGoods()
        {
            return _goods.ToList();
        }

        public List<GoodReview> GetAllReviews()
        {
            return _reviews.ToList();
        }

        public void AddGood(Good good)
        {
            good.Id = _lastGoodId++;
            _goods.Add(good);
        }

        public void Delete(int id)
        {
            var good = _goods.First(x => x.Id == id);
            _goods.Remove(good);
        }

        public Good GetGoodForUpdate(int id)
        {
            return _goods.Single(x => x.Id == id);
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
