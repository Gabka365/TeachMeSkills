using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Model.Store;
using PortalAboutEverything.Data.Repositories.DataModel;
using PortalAboutEverything.Data.Repositories.RawSql;

namespace PortalAboutEverything.Data.Repositories
{
    public class StoreRepositories : BaseRepository<Good>
    {

        public StoreRepositories(PortalDbContext db) : base(db) { }

        public List<Good> GetAllGoodsWithReviews()
        {
            return _dbContext.Goods.Include(x => x.Reviews).ToList();
        }

        public Good GetGoodByIdWithReview(int id)
        {
            var goodById = _dbContext.Goods.Include(x => x.Reviews).FirstOrDefault(x => x.Id == id);
            return goodById;
        }

        public Good GetGoodByIdWithLike(int id)
        {
            var goodById = _dbContext.Goods.Include(x => x.UsersWhoLikedTheGood).FirstOrDefault(x => x.Id == id);
            return goodById;
        }

        public List<Good> GetFavouriteGoodsByUserId(int userId)
        {
            return _dbSet.Where(good => good.UsersWhoLikedTheGood.Any(x => x.Id == userId)).ToList();
        }

        public bool AddUserWhoLikedTheGoodToGood(int id, User user)
        {
            var good = GetGoodByIdWithLike(id);

            if (good.UsersWhoLikedTheGood.Contains(user))
            {
                good.UsersWhoLikedTheGood.Remove(user);
            }
            else
            {
                good.UsersWhoLikedTheGood.Add(user);
            }
            _dbContext.SaveChanges();
            return good.UsersWhoLikedTheGood.Contains(user);
        }

        public Good GetGoodForUpdate(int id)
        {
            return _dbContext.Goods.Single(x => x.Id == id);
        }

        public void UpdateGood(Good good)
        {
            var dbGood = Get(good.Id);

            dbGood.Name = good.Name;
            dbGood.Description = good.Description;
            dbGood.Price = good.Price;

            _dbContext.SaveChanges();
        }

        public IEnumerable<TopGoodsDataModel> GetTopGoods()
        {
            return CustomSqlQuery<TopGoodsDataModel>(SqlQueryManager.GetTopGoods).ToList();
        }
    }
}
