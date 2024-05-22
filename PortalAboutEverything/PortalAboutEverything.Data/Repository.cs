using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model.Abstract;

namespace PortalAboutEverything.Data;

public class Repository<T> where T : DbModel
{
    private readonly PortalDbContext _dbContext;

    public Repository(PortalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual List<T> GetAll()
    {
        return _dbContext.Set<T>().ToList();
    }

    public virtual T? Get(Guid id)
    {
        return _dbContext.Set<T>().FirstOrDefault(f => f.Id == id);
    }

    public virtual void Create(T model)
    {
        model.Id = Guid.NewGuid();
        _dbContext.Set<T>().Add(model);

        _dbContext.SaveChanges();
    }

    public virtual void Delete(Guid id)
    {
        var item = Get(id);

        if (item is null)
        {
            return;
        }

        _dbContext.Set<T>().Remove(item);

        _dbContext.SaveChanges();
    }
}