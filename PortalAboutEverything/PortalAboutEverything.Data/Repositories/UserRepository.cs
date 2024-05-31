using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(PortalDbContext dbContext) : base(dbContext) { }

        public bool Exist(string login)
            => _dbSet.Any(x => x.UserName == login);

        public User? GetByLoginAndPasswrod(string login, string password)
        {
            return _dbSet
                .FirstOrDefault(x => x.UserName == login && x.Password == password);
        }

        public User? GetWithFavoriteBoardGames(int id)
             => _dbSet
            .Include(user => user.FavoriteBoardsGames)
            .Single(user => user.Id == id);

        public void UpdatePermission(int userId, Permission userPermission)
        {
            var user = Get(userId);
            user.Permission = userPermission;
            _dbContext.SaveChanges();
        }
    }
}
