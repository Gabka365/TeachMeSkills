namespace PortalAboutEverything.Data.Model.VideoLibrary;

public sealed class Video : BaseModel
{
    public string FilePath { get; set; }
    public double Duration { get; set; }
    public bool IsLiked { get; set; }
    public Folder Folder { get; set; }
}