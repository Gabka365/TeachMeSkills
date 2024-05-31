using PortalAboutEverything.Data.Enums;

namespace PortalAboutEverything.Models.User
{
    public class UserPermissionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Permission Permission { get; set; }
    }
}
