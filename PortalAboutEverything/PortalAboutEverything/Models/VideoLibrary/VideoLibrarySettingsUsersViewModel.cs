using PortalAboutEverything.Data.Enums;

namespace PortalAboutEverything.Models.VideoLibrary;

public class VideoLibrarySettingsUsersViewModel
{
    public List<VideoLibraryUserRoleViewModel> Users { get; set; }
    public List<UserRole> AvailableRoles { get; set; }
}