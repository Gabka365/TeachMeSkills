using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Variables.VideoLibrary;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace PortalAboutEverything.Data.Services.VideoLibrary;

public class FfMpegService
{
    public async Task GenerateThumbnail(VideoInfo videoInfo)
    {
        await CheckFfmpegFiles();
        
        var thumbnailFolder = Path.Combine(GlobalVariables.ThumbnailsFolder, videoInfo.Id.ToString());

        if (!Directory.Exists(thumbnailFolder))
        {
            Directory.CreateDirectory(thumbnailFolder);
        }

        var smallThumbnailFileName = Path.Combine(thumbnailFolder, GlobalVariables.SmallThumbnailFileName);
        var largeThumbnailFileName = Path.Combine(thumbnailFolder, GlobalVariables.LargeThumbnailFileName);

        var isSmallExists = File.Exists(smallThumbnailFileName);
        var isLargeExists = File.Exists(largeThumbnailFileName);

        if (isSmallExists && isLargeExists) return;

        Console.WriteLine($"Запущено создание превью для файла {Path.GetFileName(videoInfo.FilePath)}");

        var duration = videoInfo.Duration;
        var thumbnailTime = TimeSpan.FromSeconds(duration * 0.5).ToString(@"hh\:mm\:ss");
        var sourceVideoPath = videoInfo.FilePath;

        if (!isSmallExists)
        {
            await StartThumbnailGeneration(thumbnailTime, sourceVideoPath, smallThumbnailFileName);
        }

        if (!isLargeExists)
        {
            await StartThumbnailGeneration(thumbnailTime, sourceVideoPath, largeThumbnailFileName);
        }

        Console.WriteLine($"Создание превью для файла {Path.GetFileName(videoInfo.FilePath)} завершено");
    }

    public async Task<double> GetVideoDuration(string filePath)
    {
        await CheckFfmpegFiles();

        var mediaInfo = await FFmpeg.GetMediaInfo(filePath);
        return mediaInfo.Duration.TotalSeconds;
    }

    private static async Task CheckFfmpegFiles()
    {
        if (!Directory.Exists(GlobalVariables.FfmpegFolder)) Directory.CreateDirectory(GlobalVariables.FfmpegFolder);
        
        FFmpeg.SetExecutablesPath(GlobalVariables.FfmpegFolder);

        if (!File.Exists(GlobalVariables.FfmpegFileName) || !File.Exists(GlobalVariables.FfprobeFileName))
        {
            Console.WriteLine(
                "Не найден один или несколько необходимых файлов FFmpeg для работы сервера. Запущена загрузка недостающих файлов");
            
            await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official, GlobalVariables.FfmpegFolder);
            
            Console.WriteLine("Все недостающие файлы FFmpeg успешно загружены");
        }
    }

    private static async Task StartThumbnailGeneration(string thumbnailTime, string sourceVideoPath,
        string smallThumbnailFileName)
    {
        var argument = $"""
                        -ss {thumbnailTime} -i "{sourceVideoPath}" -vf scale=-1:360 -frames:v 1 "{smallThumbnailFileName}"
                        """;

        await FFmpeg.Conversions.New().Start(argument);
    }
}