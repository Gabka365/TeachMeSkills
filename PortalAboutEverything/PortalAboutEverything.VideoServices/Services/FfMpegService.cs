using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.VideoServices.Models;
using PortalAboutEverything.VideoServices.Variables;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace PortalAboutEverything.VideoServices.Services;

public class FfMpegService
{
    private readonly VideoLibraryRepository _repository;
    private const string FFMPEG_SMALL_SCALE_ARGUMENT = " -vf scale=-1:360";
    private const string FFMPEG_THUMBNAIL_ARGUMENTS_TEMPLATE = "-ss {0} -i \"{1}\"{2} -frames:v 1 \"{3}\"";
    
    private readonly Queue<FfmpegVideoThumbnailInfo> _thumbnailProcessQueue = new ();
    private readonly List<Task> _generatingTasks = [];

    private bool _isGenerating;

    public FfMpegService(VideoLibraryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task GenerateThumbnailAsync(List<VideoInfo> newVideoInfos)
    {
        foreach (var newVideoInfo in newVideoInfos)
        {
            var thumbnailInfo = await GenerateQueueProcessInfo(newVideoInfo);
            _thumbnailProcessQueue.Enqueue(thumbnailInfo);
        }

        if (!_isGenerating)
        {
            //Можно ли так делать или лучше через Task.Run
            _ = StartGeneratingThumbnails();
        }
    }

    private async Task<double> GetVideoDurationAsync(string filePath)
    {
        await CheckFfmpegFilesAsync();

        var mediaInfo = await FFmpeg.GetMediaInfo(filePath);
        return mediaInfo.Duration.TotalSeconds;
    }

    private async Task StartGeneratingThumbnails()
    {
        _isGenerating = true;
        
        while (_thumbnailProcessQueue.Count != 0)
        {
            var thumbnailProcessInfo = _thumbnailProcessQueue.Dequeue();

            var generatingTask = StartThumbnailGenerationAsync(thumbnailProcessInfo);

            if (_generatingTasks.Count <= GlobalVariables.ThumbnailGenerationMaxParallelProcesses)
            {
                _generatingTasks.Add(generatingTask);
                continue;
            }

            var completedTask = await Task.WhenAny(_generatingTasks);
            _generatingTasks.Remove(completedTask);
        }

        await Task.WhenAll(_generatingTasks);

        _isGenerating = false;
    }

    private async Task<FfmpegVideoThumbnailInfo> GenerateQueueProcessInfo(VideoInfo videoInfo)
    {
        videoInfo.Duration = await GetVideoDurationAsync(videoInfo.FilePath);
        var thumbnailTime = TimeSpan.FromSeconds(videoInfo.Duration * 0.5).ToString(@"hh\:mm\:ss");
        var thumbnailFolder = videoInfo.Id.ToString();
        
        return new FfmpegVideoThumbnailInfo
        {
            ThumbnailTime = thumbnailTime,
            SourceVideoPath = videoInfo.FilePath,
            ThumbnailFolder = thumbnailFolder,
            VideoInfo = videoInfo
        };
    }

    private static async Task CheckFfmpegFilesAsync()
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

    private async Task StartThumbnailGenerationAsync(FfmpegVideoThumbnailInfo thumbnailInfo)
    {
        //Может ли получиться, что сразу несколько потоков попытаются создать папку
        if (!Directory.Exists(GlobalVariables.ThumbnailsFolder))
        {
            Directory.CreateDirectory(GlobalVariables.ThumbnailsFolder);
        }

        var thumbnailFolder = Path.Combine(GlobalVariables.ThumbnailsFolder, thumbnailInfo.ThumbnailFolder);

        if (!Directory.Exists(thumbnailFolder))
        {
            Directory.CreateDirectory(thumbnailFolder);
        }
        
        var smallThumbnailFileName = Path.Combine(thumbnailFolder, GlobalVariables.SmallThumbnailFileName);
        var largeThumbnailFileName = Path.Combine(thumbnailFolder, GlobalVariables.LargeThumbnailFileName);

        var isSmallExists = File.Exists(smallThumbnailFileName);
        var isLargeExists = File.Exists(largeThumbnailFileName);

        if (isSmallExists && isLargeExists) return;

        string ffmpegArguments;
        
        if (!isSmallExists)
        {
            ffmpegArguments = string.Format(FFMPEG_THUMBNAIL_ARGUMENTS_TEMPLATE, thumbnailInfo.ThumbnailTime,
                thumbnailInfo.SourceVideoPath, FFMPEG_SMALL_SCALE_ARGUMENT, smallThumbnailFileName);

            await StartFfmpegProcess(ffmpegArguments);
        }

        if (!isLargeExists)
        {
            ffmpegArguments = string.Format(FFMPEG_THUMBNAIL_ARGUMENTS_TEMPLATE, thumbnailInfo.ThumbnailTime,
                thumbnailInfo.SourceVideoPath, null, largeThumbnailFileName);

            await StartFfmpegProcess(ffmpegArguments);
        }
        
        _repository.Create(thumbnailInfo.VideoInfo);
    }
    
    private async Task StartFfmpegProcess(string parameters)
    {
        await FFmpeg.Conversions.New().Start(parameters);
    }
}