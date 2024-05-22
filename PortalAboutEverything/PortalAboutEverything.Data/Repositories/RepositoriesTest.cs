using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Data.Repositories
{
    public abstract class RepositoriesTest<T> where T : class
    {
        public PortalDbContext _dbContext;
        private DbSet<T> _dbSet;

        public RepositoriesTest(PortalDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Create(T repositories)
        {
            _dbSet.Add(repositories);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var repositories = _dbSet.Find(id);
            _dbSet.Remove(repositories);
            _dbContext.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public abstract void Update(T entity);
    }
}

