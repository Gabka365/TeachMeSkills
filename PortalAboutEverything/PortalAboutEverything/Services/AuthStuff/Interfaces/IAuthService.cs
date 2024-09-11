using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Services.AuthStuff.Interfaces
{
    public interface IAuthService
    {
        User GetUser();
        int GetUserId();
        Language GetUserLanguage();
        string GetUserName();
        Permission GetUserPermission();
        UserRole GetUserRole();
        bool HasPermission(Permission permission);
        bool HasRole(UserRole role);
        bool HasRoleOrHigher(UserRole role);
        bool IsAdmin();
        bool IsAuthenticated();
    }
}