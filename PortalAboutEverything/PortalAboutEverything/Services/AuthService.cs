using PortalAboutEverything.Data.Enums;
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

        public string GetUserName()
            => GetClaimValue("Name");

        public UserRole GetUserRole()
        {
            var userRole = GetClaimValue("Role");
            return Enum.Parse<UserRole>(userRole);
        }

        public Permission GetUserPermission()
        {
            var userRole = GetClaimValue("Permission");
            return Enum.Parse<Permission>(userRole);
        }

        public bool HasRoleOrHigher(UserRole role) {
            return GetUserRole() >= role;
        }

        public int GetUserId()
        {
            var userIdText = GetClaimValue("Id");
            var userId = int.Parse(userIdText);
            return userId;
        }

        public bool IsAdmin()
        {
            return IsAuthenticated() && GetUserRole() == UserRole.Admin;
        }

        private string GetClaimValue(string claimType)
            => _httpContextAccessor
                .HttpContext!
                .User
                .Claims
                .First(x => x.Type == claimType)
                .Value;
    }
}
