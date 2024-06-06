using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PortalAboutEverything.Data.Model.VideoLibrary;
using PortalAboutEverything.Data.Repositories.VideoLibrary;
using PortalAboutEverything.VideoServices.Enums;
using PortalAboutEverything.VideoServices.Models;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace PortalAboutEverything.VideoServices.Services;

public class FfmpegService : BackgroundService
{
    private const string FFMPEG_SMALL_SCALE_ARGUMENT = " -vf scale=-1:360";
    private const string FFMPEG_THUMBNAIL_ARGUMENTS_TEMPLATE = "-ss {0} -i \"{1}\"{2} -frames:v 1 \"{3}\"";
    private readonly object _lockObject = new();
    private readonly ILogger<FfmpegService> _logger;
    private readonly List<Task> _processTasks = [];
    private readonly VideoProcessQueueService _queue;
    private readonly IServiceProvider _serviceProvider;

    public FfmpegService(IServiceProvider serviceProvider, VideoProcessQueueService queue,
        ILogger<FfmpegService> logger)
    {
        _serviceProvider = serviceProvider;
        _queue = queue;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _ = StartListenProcess(stoppingToken);
        return Task.CompletedTask;
    }

    private async Task StartListenProcess(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_queue.VideoToProcessQueue.IsEmpty)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                continue;
            }
            
            await CheckFfmpegFiles();

            while (!_queue.VideoToProcessQueue.IsEmpty)
            {
                var isSuccess = _queue.VideoToProcessQueue.TryDequeue(out var videoInfo);

                if (!isSuccess || videoInfo is null)
                {
                    continue;
                }

                if (_processTasks.Count <= VideoServiceVariables.MaxDegreeOfParallelism)
                {
                    _processTasks.Add(ThumbnailGenerationTask(videoInfo, stoppingToken));
                    continue;
                }

                var completedTask = await Task.WhenAny(_processTasks);
                _processTasks.Remove(completedTask);
            }

            await Task.WhenAll(_processTasks);
        }
    }

    private async Task ThumbnailGenerationTask(VideoProcessInfo videoInfo, CancellationToken token)
    {
        using var scope = _serviceProvider.CreateScope();
        var folderRepository = scope.ServiceProvider.GetRequiredService<FolderRepository>();
        var videoRepository = scope.ServiceProvider.GetRequiredService<VideoRepository>();

        Video? video;

        switch (videoInfo.VideoProcessingType)
        {
            default:
            case VideoProcessingType.NewThumbnail:
                var folder = folderRepository.Get(videoInfo.Folder.Id);

                if (folder is null)
                {
                    throw new KeyNotFoundException();
                }

                video = videoRepository.Create(new Video
                {
                    FilePath = videoInfo.VideoPath,
                    Duration = await GetVideoDuration(videoInfo.VideoPath),
                    IsLiked = false,
                    Folder = folder
                });

                var isSuccess = await GenerateThumbnail(video, token);

                if (isSuccess)
                {
                }

                break;
            case VideoProcessingType.MissingThumbnail:
                video = videoRepository.Get(videoInfo.VideoId);

                if (video is null)
                {
                    return;
                }

                await GenerateThumbnail(video, token);
                break;
        }
    }

    private async Task<bool> GenerateThumbnail(Video video, CancellationToken stoppingToken)
    {
        if (!Directory.Exists(VideoServiceVariables.ThumbnailsFolder))
        {
            lock (_lockObject)
            {
                if (!Directory.Exists(VideoServiceVariables.ThumbnailsFolder))
                {
                    Directory.CreateDirectory(VideoServiceVariables.ThumbnailsFolder);
                }
            }
        }

        var thumbnailFolder = Path.Combine(VideoServiceVariables.ThumbnailsFolder, video.Id.ToString());

        if (!Directory.Exists(thumbnailFolder))
        {
            Directory.CreateDirectory(thumbnailFolder);
        }

        var smallThumbnailFileName = Path.Combine(thumbnailFolder, VideoServiceVariables.SmallThumbnailFileName);
        var largeThumbnailFileName = Path.Combine(thumbnailFolder, VideoServiceVariables.LargeThumbnailFileName);

        var isSmallExists = File.Exists(smallThumbnailFileName);
        var isLargeExists = File.Exists(largeThumbnailFileName);

        if (isSmallExists && isLargeExists)
        {
            return true;
        }

        _logger.LogInformation("Запущено создание превью для файла {FilePath}", Path.GetFileName(video.FilePath));

        var duration = video.Duration;
        var thumbnailTime = TimeSpan.FromSeconds(duration * 0.5).ToString(@"hh\:mm\:ss");
        var sourceVideoPath = video.FilePath;

        try
        {
            string ffmpegArguments;

            if (!isSmallExists)
            {
                ffmpegArguments = string.Format(FFMPEG_THUMBNAIL_ARGUMENTS_TEMPLATE, thumbnailTime,
                    sourceVideoPath, FFMPEG_SMALL_SCALE_ARGUMENT, smallThumbnailFileName);

                await StartFfmpegProcess(ffmpegArguments, stoppingToken);
            }

            if (!isLargeExists)
            {
                ffmpegArguments = string.Format(FFMPEG_THUMBNAIL_ARGUMENTS_TEMPLATE, thumbnailTime,
                    sourceVideoPath, null, largeThumbnailFileName);

                await StartFfmpegProcess(ffmpegArguments, stoppingToken);
            }
        }
        catch (Exception e)
        {
            _logger.LogError("Создание превью завершилось с ошибкой. Сообщение ошибки: {Message}", e.Message);

            return false;
        }

        _logger.LogInformation("Создание превью для файла {Path} завершено", Path.GetFileName(video.FilePath));
        return true;
    }

    private static async Task StartFfmpegProcess(string parameters, CancellationToken stoppingToken)
    {
        await FFmpeg.Conversions.New().Start(parameters, stoppingToken);
    }

    private static async Task<double> GetVideoDuration(string filePath)
    {
        await CheckFfmpegFiles();

        var mediaInfo = await FFmpeg.GetMediaInfo(filePath);
        return mediaInfo.Duration.TotalSeconds;
    }

    private static async Task CheckFfmpegFiles()
    {
        if (!Directory.Exists(VideoServiceVariables.FfmpegFolder))
        {
            Directory.CreateDirectory(VideoServiceVariables.FfmpegFolder);
        }

        FFmpeg.SetExecutablesPath(VideoServiceVariables.FfmpegFolder);

        if (!File.Exists(VideoServiceVariables.FfmpegFileName) || !File.Exists(VideoServiceVariables.FfprobeFileName))
        {
            Console.WriteLine(
                "Не найден один или несколько необходимых файлов FFmpeg для работы сервера. Запущена загрузка недостающих файлов");

            await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official, VideoServiceVariables.FfmpegFolder);

            Console.WriteLine("Все недостающие файлы FFmpeg успешно загружены");
        }
    }
}