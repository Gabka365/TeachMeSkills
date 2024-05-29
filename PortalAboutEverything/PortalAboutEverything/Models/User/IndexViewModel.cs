using PortalAboutEverything.Data.Enums;

namespace PortalAboutEverything.Models.User
{
    public class IndexViewModel
    {
        public List<UserPermissionViewModel> Users { get; set; }
        
        public List<Permission> AvailablePermissions { get; set; }
    }
}
