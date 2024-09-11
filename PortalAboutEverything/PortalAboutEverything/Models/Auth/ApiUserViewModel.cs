using PortalAboutEverything.Data.Enums;

namespace PortalAboutEverything.Models.Auth
{
    public class ApiUserViewModel
    {
        public string Name { get; set; }
        public string Permissions { get; set; }
        public string Roles { get; set; }
    }
}
