using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public abstract class BaseRepository<DbModel>
        where DbModel : BaseModel
    {
        protected PortalDbContext _dbContext;
        protected DbSet<DbModel> _dbSet;

        public BaseRepository(PortalDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<DbModel>();
        }

        public virtual List<DbModel> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual DbModel? Get(int id)
        {
            return _dbSet.FirstOrDefault(dbModel => dbModel.Id == id);
        }

        public virtual DbModel Create(DbModel model)
        {
            _dbSet.Add(model);

            _dbContext.SaveChanges();

            return model;
        }

        public virtual void Delete(int id)
        {
            var model = Get(id);

            if (model is null)
            {
                throw new KeyNotFoundException();
            }

            Delete(model);
        }

        public virtual void Delete(DbModel model)
        {
            _dbSet.Remove(model);
            _dbContext.SaveChanges();
        }
    }
}
