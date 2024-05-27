using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;

namespace PortalAboutEverything.Services
{
    public class AuthService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private UserRepository _userRepository;

        public AuthService(IHttpContextAccessor httpContextAccessor, UserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public bool IsAuthenticated()
            => _httpContextAccessor.HttpContext!.User.Identity?.IsAuthenticated ?? false;

        public User GetUser()
        {
            var userId = GetUserId();
            return _userRepository.Get(userId)!;
        }

        public User GetUserWithFavoriteBoardGames()
        {
            var userId = GetUserId();
            return _userRepository.GetWithFavoriteBoardGames(userId)!;
        }

        public string GetUserName()
        {
            var userName = _httpContextAccessor
                .HttpContext!
                .User
                .Claims
                .First(x => x.Type == "Name")
                .Value;
            return userName;
        }

        public int GetUserId()
        {
            var userIdText = _httpContextAccessor
                .HttpContext!
                .User
                .Claims
                .First(x => x.Type == "Id")
                .Value;
            var userId = int.Parse(userIdText);
            return userId;
        }
    }
}
