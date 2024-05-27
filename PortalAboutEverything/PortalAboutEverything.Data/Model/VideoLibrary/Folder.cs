namespace PortalAboutEverything.Data.Model.VideoLibrary;

public class Folder : BaseModel
{
    public string Name { get; set; }
    public virtual List<Video> Videos { get; set; }
}