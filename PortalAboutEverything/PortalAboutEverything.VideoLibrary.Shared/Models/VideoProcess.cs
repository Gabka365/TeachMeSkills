using PortalAboutEverything.VideoLibrary.Shared.Enums;

namespace PortalAboutEverything.VideoLibrary.Shared.Models;

public class VideoProcess
{
    public Guid Id { get; init; }
    public required Guid VideoId { get; set; }
    public required string VideoPath { get; init; }
    public required double Duration { get; set; }
    public required Guid FolderId { get; init; }
    public required VideoProcessingType VideoProcessingType { get; init; }
}