namespace PortalAboutEverything.Data.Model;

public class VideoInfo
{
    public Guid Id { get; set; }
    public string FilePath { get; set; }
    public double Duration { get; set; }
    public bool IsLiked { get; set; }
}