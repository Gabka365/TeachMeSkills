using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.VideoServices.Models;

public class FfmpegVideoThumbnailInfo
{
    public required string ThumbnailTime { get; init; }
    public required string SourceVideoPath { get; init; }
    public required string ThumbnailFolder { get; init; }
    public required VideoInfo VideoInfo { get; set; }
}