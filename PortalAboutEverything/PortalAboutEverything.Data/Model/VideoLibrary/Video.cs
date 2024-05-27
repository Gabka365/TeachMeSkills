namespace PortalAboutEverything.Data.Model.VideoLibrary;

public class Video : BaseModel
{
    public string FilePath { get; set; }
    public double Duration { get; set; }
    public bool IsLiked { get; set; }
    public virtual Folder Folder { get; set; }
}