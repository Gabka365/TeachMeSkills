using PortalAboutEverything.Data.Enums;

namespace PortalAboutEverything.Models.VideoLibrary;

public class VideoLibraryUserRoleViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UserRole Role { get; set; }
}