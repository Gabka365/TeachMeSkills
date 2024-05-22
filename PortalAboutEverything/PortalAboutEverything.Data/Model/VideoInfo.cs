using PortalAboutEverything.Data.Model.Abstract;

namespace PortalAboutEverything.Data.Model;

public class VideoInfo : DbModel
{
    public Guid Id { get; set; }
    public string FilePath { get; set; }
    public double Duration { get; set; }
    public bool IsLiked { get; set; }
}