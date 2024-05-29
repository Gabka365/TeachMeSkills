using PortalAboutEverything.Data.Model.VideoLibrary;
using PortalAboutEverything.VideoServices.Enums;

namespace PortalAboutEverything.VideoServices.Models;

public class VideoProcessInfo
{
    public required int VideoId { get; init; }
    public required string VideoPath { get; init; }
    public required Folder Folder { get; init; }
    public required VideoProcessingType VideoProcessingType { get; init; }
}