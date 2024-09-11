using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories.Interfaces
{
    public interface IBaseRepository<DbModel> where DbModel : BaseModel
    {
        bool Any();
        DbModel Create(DbModel model);
        IQueryable<T> CustomSqlQuery<T>(string sqlFileName);
        void Delete(DbModel model);
        void Delete(int id);
        DbModel? Get(int id);
        List<DbModel> GetAll();
    }
}