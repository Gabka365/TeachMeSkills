using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        void AddMovieToMoviesFan(Movie movie, int userId);
        void DeleteMovieFromMoviesFan(Movie movie, int userId);
        bool Exist(string login);
        User? GetByLoginAndPasswrod(string login, string password);
        Language GetLanguage(int userId);
        User? GetWithFavoriteBoardGames(int id);
        User? GetWithFavoriteMovies(int id);
        void UpdatePermission(int userId, Permission userPermission);
    }
}