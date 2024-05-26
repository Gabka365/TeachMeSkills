namespace PortalAboutEverything.Data.Model.VideoLibrary;

public sealed class Folder : BaseModel
{
    public string Name { get; set; }
    public List<Video> Videos { get; set; }
}