using PortalAboutEverything.Data.Model.VideoLibrary;
using PortalAboutEverything.Data.Repositories.VideoLibrary;
using PortalAboutEverything.VideoServices.Enums;
using PortalAboutEverything.VideoServices.Models;

namespace PortalAboutEverything.VideoServices.Services;

public class VideoFileSystemService
{
    private readonly FolderRepository _folderRepository;
    private readonly VideoProcessQueueService _queue;
    private readonly VideoRepository _videoRepository;
    private bool _isExporting;

    private bool _isScanning;

    public VideoFileSystemService(VideoProcessQueueService queue, FolderRepository folderRepository,
        VideoRepository videoRepository)
    {
        _queue = queue;
        _folderRepository = folderRepository;
        _videoRepository = videoRepository;
    }

    public void StartUpdateLibrary()
    {
        if (_isScanning)
        {
            return;
        }

        _isScanning = true;

        CheckFoldersExists();

        var foldersInDb = _folderRepository.GetAllWithVideos();
        var foldersInSourceFolder = GetFoldersInFolder(VideoServiceVariables.UnsortedVideosFolder);
        var foldersInThumbnailFolder = GetFoldersInFolder(VideoServiceVariables.ThumbnailsFolder);

        CheckMissingFolders(foldersInSourceFolder, foldersInDb);
        CheckNewFolders(foldersInSourceFolder, foldersInDb);

        var videosInDb = _videoRepository.GetAll();

        CheckMissingFiles(foldersInSourceFolder, videosInDb);
        CheckMissingThumbnailFolders(foldersInThumbnailFolder, videosInDb);
        CheckMissingThumbnails(foldersInThumbnailFolder, videosInDb);
        CheckNewVideosInFolders(foldersInSourceFolder, videosInDb);

        _isScanning = false;
    }

    public string? GetThumbnailPath(int id, ThumbnailSizeEnum thumbnailSize)
    {
        var thumbnailFolder = Path.Combine(VideoServiceVariables.ThumbnailsFolder, id.ToString());

        if (!Directory.Exists(thumbnailFolder))
        {
            return null;
        }

        string thumbnailPath;

        switch (thumbnailSize)
        {
            default:
            case ThumbnailSizeEnum.Small:
                thumbnailPath = Path.Combine(VideoServiceVariables.ThumbnailsFolder, id.ToString(),
                    VideoServiceVariables.SmallThumbnailFileName);
                return File.Exists(thumbnailPath) ? thumbnailPath : null;
            case ThumbnailSizeEnum.Large:
                thumbnailPath = Path.Combine(VideoServiceVariables.ThumbnailsFolder, id.ToString(),
                    VideoServiceVariables.LargeThumbnailFileName);
                return File.Exists(thumbnailPath) ? thumbnailPath : null;
        }
    }

    public async Task DeleteVideo(int id)
    {
        var video = _videoRepository.Get(id);

        if (video is null)
        {
            throw new KeyNotFoundException();
        }

        while (true)
        {
            try
            {
                if (File.Exists(video.FilePath))
                {
                    File.Delete(video.FilePath);
                }

                break;
            }
            catch (Exception)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        DeleteThumbnailFolder(video.Id);

        _videoRepository.Delete(video);
    }

    public void ExportLikedVideos()
    {
        if (_isExporting)
        {
            return;
        }

        _isExporting = true;

        var likedVideos = _videoRepository.GetAllWithFolders(true);

        foreach (var video in likedVideos)
        {
            var exportVideoFolder = Path.Combine(VideoServiceVariables.ExportedVideosFolder, video.Folder.Name);

            if (!Directory.Exists(exportVideoFolder))
            {
                Directory.CreateDirectory(exportVideoFolder);
            }

            var newVideoPath = Path.Combine(exportVideoFolder, Path.GetFileName(video.FilePath));

            File.Move(video.FilePath, newVideoPath);

            DeleteThumbnailFolder(video.Id);

            _videoRepository.Delete(video);
        }

        _isExporting = false;
    }

    private void CheckMissingFiles(List<string> foldersInSourceFolder, List<Video> videosInDb)
    {
        var videosInSourceFolder = foldersInSourceFolder
            .Select(GetVideosInFolder)
            .SelectMany(video => video)
            .ToList();
        var missingFiles = videosInDb.FindAll(video => !videosInSourceFolder.Contains(video.FilePath));

        if (missingFiles.Count == 0)
        {
            return;
        }

        _videoRepository.DeleteMany(missingFiles);
    }

    private static void DeleteThumbnailFolder(string folder)
    {
        var thumbnailFolder = Path.Combine(VideoServiceVariables.ThumbnailsFolder, folder);

        if (Directory.Exists(thumbnailFolder))
        {
            Directory.Delete(thumbnailFolder, true);
        }
    }

    private static void DeleteThumbnailFolder(int id)
    {
        var thumbnailFolder = Path.Combine(VideoServiceVariables.ThumbnailsFolder, id.ToString());

        if (Directory.Exists(thumbnailFolder))
        {
            Directory.Delete(thumbnailFolder, true);
        }
    }

    private static void CheckFoldersExists()
    {
        if (!Directory.Exists(VideoServiceVariables.UnsortedVideosFolder))
        {
            Directory.CreateDirectory(VideoServiceVariables.UnsortedVideosFolder);
        }

        if (!Directory.Exists(VideoServiceVariables.ThumbnailsFolder))
        {
            Directory.CreateDirectory(VideoServiceVariables.UnsortedVideosFolder);
        }
    }

    private static List<string> GetFoldersInFolder(string folder)
    {
        return Directory
            .GetDirectories(folder, "*", SearchOption.TopDirectoryOnly)
            .Select(GetFolderNameFromFolderPath)
            .ToList();
    }

    private static List<string> GetVideosInFolder(string folder)
    {
        var folderPath = Path.Combine(VideoServiceVariables.UnsortedVideosFolder, folder);
        return Directory.EnumerateFiles(folderPath, "*.mp4", SearchOption.AllDirectories).ToList();
    }

    private void CheckMissingFolders(List<string> foldersInSourceFolder, List<Folder> foldersInDb)
    {
        var missingFolders = foldersInDb
            .FindAll(folder => !foldersInSourceFolder
                .Contains(folder.Name));

        if (missingFolders.Count != 0)
        {
            return;
        }

        foreach (var video in missingFolders.SelectMany(missingFolder => missingFolder.Videos))
        {
            DeleteThumbnailFolder(video.Id);
        }

        _folderRepository.DeleteFolders(missingFolders);
    }

    private void CheckNewFolders(List<string> foldersInSourceFolder, List<Folder> foldersInDb)
    {
        var newFolders =
            foldersInSourceFolder
                .FindAll(folder => !foldersInDb
                    .Select(folderInDb => folderInDb.Name)
                    .Contains(folder));

        if (newFolders.Count != 0)
        {
            ProcessNewFolders(newFolders);
        }
    }

    private void ProcessNewFolders(List<string> newFolders)
    {
        foreach (var newFolder in newFolders)
        {
            var folder = _folderRepository.Get(GetFolderNameFromFolderPath(newFolder)) ?? _folderRepository.AddFolder(
                new Folder
                {
                    Name = GetFolderNameFromFolderPath(newFolder),
                    Videos = []
                });

            var videoInfos = GetVideosInFolder(newFolder)
                .Select(video => new VideoProcessInfo
                {
                    VideoPath = video,
                    Folder = folder,
                    VideoId = int.MaxValue,
                    VideoProcessingType = VideoProcessingType.NewThumbnail
                })
                .ToList();

            _queue.EnqueueMany(videoInfos);
        }
    }

    private static string GetFolderNameFromFolderPath(string folderPath)
    {
        return Path.GetFileName(folderPath);
    }

    private static void CheckMissingThumbnailFolders(List<string> foldersInThumbnailFolder, List<Video> videosInDb)
    {
        var missingThumbnailFolder = foldersInThumbnailFolder
            .FindAll(folder => !videosInDb
                .Select(dbFolder => dbFolder.Id.ToString())
                .Contains(folder));

        if (missingThumbnailFolder.Count == 0)
        {
            return;
        }

        foreach (var folder in missingThumbnailFolder)
        {
            DeleteThumbnailFolder(folder);
        }
    }

    private void CheckMissingThumbnails(List<string> foldersInThumbnailFolder, List<Video> videosInDb)
    {
        var missingThumbnails = videosInDb
            .FindAll(video => !foldersInThumbnailFolder
                .Contains(video.Id.ToString()))
            .ToList();

        if (missingThumbnails.Count == 0)
        {
            return;
        }

        var videoInfos = missingThumbnails.Select(video => new VideoProcessInfo
            {
                VideoPath = video.FilePath,
                Folder = video.Folder,
                VideoId = video.Id,
                VideoProcessingType = VideoProcessingType.MissingThumbnail
            })
            .ToList();

        _queue.EnqueueMany(videoInfos);
    }

    private void CheckNewVideosInFolders(List<string> foldersInSourceFolder, List<Video> videosInDb)
    {
        List<VideoProcessInfo> videoInfos = [];

        foreach (var sourceFolder in foldersInSourceFolder)
        {
            var folder = _folderRepository.Get(sourceFolder) ??
                         _folderRepository.AddFolder(
                             new Folder
                             {
                                 Name = sourceFolder,
                                 Videos = []
                             });

            var videosInFolder = GetVideosInFolder(sourceFolder);
            var newVideos = videosInFolder
                .FindAll(video =>
                    !videosInDb
                        .Select(videoInDb => videoInDb.FilePath)
                        .Contains(video));

            if (newVideos.Count == 0)
            {
                continue;
            }

            videoInfos.AddRange(newVideos.Select(video => new VideoProcessInfo
            {
                VideoPath = video,
                Folder = folder,
                VideoId = int.MaxValue,
                VideoProcessingType = VideoProcessingType.NewThumbnail
            }));

            _queue.EnqueueMany(videoInfos);
        }
    }
}