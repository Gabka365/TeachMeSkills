using Microsoft.EntityFrameworkCore;
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
        private PortalDbContext _dbContext;

        public StoreRepositories(PortalDbContext db)
        {
            _dbContext = db;
        }

        public List<Good> GetAllGoods()
        {
            return _dbContext.Goods.ToList();
        }

        public List<Good> GetAllGoodsWithReviews()
        {
            return _dbContext.Goods.Include(x => x.Reviews).ToList();
        }

        public Good GetGoodByIdWithReview(int id)
        {
            var goodById = _dbContext.Goods.Include(x => x.Reviews).FirstOrDefault(x => x.Id == id);
            return goodById;
        }

        public Good GetGoodById(int id)
        {
            var goodById = _dbContext.Goods.FirstOrDefault(x => x.Id == id);
            return goodById;
        }

        public void AddGood(Good good)
        {
            _dbContext.Goods.Add(good);
            _dbContext.SaveChanges();
        }

        //public void Delete(int id)
        //{
        //    var good = _dbContext.Goods.First(x => x.Id == id);
        //    _dbContext.Goods.Remove(good);
        //    _dbContext.SaveChanges();
        //}

        public void Delete(int id)
        {
            var good = _dbContext.Goods.Include(x => x.Reviews).FirstOrDefault(x => x.Id == id);

            if (good != null)
            {
                _dbContext.Goods.Remove(good);
                _dbContext.SaveChanges();
            }
        }

        public Good GetGoodForUpdate(int id)
        {
            return _dbContext.Goods.Single(x => x.Id == id);

        }

        public void UpdateGood(Good good)
        {
            var dbGood = GetGoodById(good.Id);


            dbGood.Name = good.Name;
            dbGood.Description = good.Description;
            dbGood.Price = good.Price;

            _dbContext.SaveChanges();
        }
    }
}
