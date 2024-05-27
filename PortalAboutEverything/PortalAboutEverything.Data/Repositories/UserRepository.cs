using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(PortalDbContext dbContext) : base(dbContext) { }

        public User? GetByLoginAndPasswrod(string login, string password)
        {
            return _dbSet
                .FirstOrDefault(x => x.UserName == login && x.Password == password);
        }

        public User? GetWithFavoriteBoardGames(int id)
             => _dbSet
            .Include(user => user.FavoriteBoardsGames)
            .Single(user => user.Id == id);
    }
}
