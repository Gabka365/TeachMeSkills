using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.VideoServices.Enums;
using PortalAboutEverything.VideoServices.Variables;

namespace PortalAboutEverything.VideoServices.Services;

public class VideoService
{
    private readonly FfMpegService _ffMpegService;
    private readonly VideoLibraryRepository _repository;
    private bool _isScanning;

    public VideoService(VideoLibraryRepository repository, FfMpegService ffMpegService)
    {
        _repository = repository;
        _ffMpegService = ffMpegService;
    }

    public async Task ScanUnsortedVideoFolder()
    {
        if (_isScanning) return;

        _isScanning = true;

        if (!Directory.Exists(GlobalVariables.UnsortedVideosFolder))
        {
            Directory.CreateDirectory(GlobalVariables.UnsortedVideosFolder);
        }

        var videosInDb = _repository.GetAll();
        var videosInFolder = Directory
            .EnumerateFiles(GlobalVariables.UnsortedVideosFolder, "*.mp4", SearchOption.AllDirectories).ToList();
        
        CheckThumbnailFolder(videosInDb);
        CheckMissingVideos(videosInDb, videosInFolder);
        await CheckMissingThumbnailFiles(videosInDb);
        await CheckNewVideos(videosInFolder, videosInDb);

        _isScanning = false;
    }

    public string GetThumbnailPath(Guid id, ThumbnailSizeEnum thumbnailSize)
    {
        string thumbnailPath;

        switch (thumbnailSize)
        {
            default:
            case ThumbnailSizeEnum.Small:
                thumbnailPath = Path.Combine(GlobalVariables.ThumbnailsFolder, id.ToString(),
                    GlobalVariables.SmallThumbnailFileName);
                break;
            case ThumbnailSizeEnum.Large:
                thumbnailPath = Path.Combine(GlobalVariables.ThumbnailsFolder, id.ToString(),
                    GlobalVariables.LargeThumbnailFileName);
                break;
        }

        return thumbnailPath;
    }

    public void DeleteVideo(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        File.Delete(filePath);
    }

    public static void DeleteThumbnailFolder(string id)
    {
        var thumbnailFolder = Path.Combine(GlobalVariables.ThumbnailsFolder, id);
        
        if (!Directory.Exists(thumbnailFolder))
        {
            return;
        }
        
        Directory.Delete(thumbnailFolder, true);
    }

    private async Task CheckMissingThumbnailFiles(List<VideoInfo> videosInDb)
    {
        var videosWithMissingThumbnails = videosInDb.ToList().Where(videoInfo =>
                !Directory.Exists(Path.Combine(GlobalVariables.ThumbnailsFolder, videoInfo.Id.ToString())) ||
                !File.Exists(
                    Path.Combine(GlobalVariables.ThumbnailsFolder, videoInfo.Id.ToString(),
                        GlobalVariables.SmallThumbnailFileName)) ||
                !File.Exists(
                    Path.Combine(GlobalVariables.ThumbnailsFolder, videoInfo.Id.ToString(),
                        GlobalVariables.LargeThumbnailFileName)))
            .ToList();

        await _ffMpegService.GenerateThumbnailAsync(videosWithMissingThumbnails);
    }

    private void CheckThumbnailFolder(List<VideoInfo> videosInDb)
    {
        var thumbnailFolders = Directory.GetDirectories(GlobalVariables.ThumbnailsFolder).Select(Path.GetFileName).ToList();

        var wrongFolders =
            thumbnailFolders.Where(folder =>
                videosInDb.ToList().All(videoInfo => videoInfo.Id.ToString() != folder)).ToList();

        DeleteThumbnailsFolder(wrongFolders);
    }

    private async Task CheckNewVideos(List<string> videosInFolder, List<VideoInfo> videosInDb)
    {
        var newVideosPaths =
            videosInFolder.Where(folderVideo => videosInDb.All(dbVideo => dbVideo.FilePath != folderVideo)).ToList();

        List<VideoInfo> newVideoInfos = [];
        newVideoInfos.AddRange(newVideosPaths.Select(GenerateVideoInfo));

        await _ffMpegService.GenerateThumbnailAsync(newVideoInfos);
    }

    private void CheckMissingVideos(List<VideoInfo> videosInDb, List<string> videosInFolder)
    {
        var missingVideos =
            videosInDb.Where(dbVideo => videosInFolder.All(folderVideo => folderVideo != dbVideo.FilePath)).ToList();

        DeleteMissingVideos(missingVideos);
    }

    private void DeleteThumbnailsFolder(List<string?> wrongFolders)
    {
        foreach (var wrongFolder in wrongFolders)
        {
            if (string.IsNullOrWhiteSpace(wrongFolder))
            {
                return;
            }
            
            DeleteThumbnailFolder(wrongFolder);
        }
    }

    private void DeleteMissingVideos(List<VideoInfo> missingVideos)
    {
        foreach (var video in missingVideos)
        {
            _repository.Delete(video.Id);
        }
    }

    private VideoInfo GenerateVideoInfo(string filePath)
    {
        var id = Guid.NewGuid();

        return new VideoInfo
        {
            Id = id,
            FilePath = filePath,
            IsLiked = false
        };
    }
}