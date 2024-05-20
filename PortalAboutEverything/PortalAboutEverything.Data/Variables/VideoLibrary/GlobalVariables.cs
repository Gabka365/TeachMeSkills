namespace PortalAboutEverything.Data.Variables.VideoLibrary;

public static class GlobalVariables
{
    private static readonly string AppFolder = AppContext.BaseDirectory;
    private static readonly string ProjectFolder = Path.Combine(AppFolder, "VideoLibrary");
    private static readonly string AssetsFolder = Path.Combine(ProjectFolder, "Assets");
    
    public static readonly string ThumbnailsFolder = Path.Combine(AssetsFolder, "Thumbnails");
    public static readonly string SmallThumbnailFileName = "thumbnail-sm.jpg";
    public static readonly string LargeThumbnailFileName = "thumbnail-lg.jpg";
    
    public static readonly string FfmpegFolder = Path.Combine(ProjectFolder, "Ffmpeg");
    public static readonly string FfmpegFileName = Path.Combine(FfmpegFolder, "ffmpeg.exe");
    public static readonly string FfprobeFileName = Path.Combine(FfmpegFolder, "ffprobe.exe");

    public static readonly string UnsortedVideosFolder = Path.Combine(ProjectFolder, "Unsorted Videos");
    public static readonly string ExportedVideosFolder = Path.Combine(ProjectFolder, "Exported Videos");
}