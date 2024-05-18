using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Data.Variables.VideoLibrary;
using Xabe.FFmpeg;

namespace PortalAboutEverything.Data.Services.VideoLibrary;

public class VideoProcessorService
{
    private readonly VideoLibraryRepository _repository;
    private readonly FfMpegService _ffMpegService;

    public VideoProcessorService(VideoLibraryRepository repository, FfMpegService ffMpegService)
    {
        _repository = repository;
        _ffMpegService = ffMpegService;
    }

    public async Task ScanUnsortedVideoFolder()
    {
        if (!Directory.Exists(GlobalVariables.UnsortedVideosFolder))
        {
            Directory.CreateDirectory(GlobalVariables.UnsortedVideosFolder);
        }
        
        var videosInDb = _repository.GetAllVideos();
        var videosInFolder = Directory
            .EnumerateFiles(GlobalVariables.UnsortedVideosFolder, "*.mp4", SearchOption.AllDirectories).ToList();
        
        await CheckNewVideos(videosInFolder, videosInDb);
        CheckMissingVideos(videosInDb, videosInFolder);
        
        videosInDb = _repository.GetAllVideos();
        
        if(videosInDb.Count != 0)
        {
            CheckThumbnailFolder(videosInDb);
        }

        await CheckMissingThumbnailFiles(videosInDb);
    }

    private async Task CheckMissingThumbnailFiles(List<VideoInfo> videosInDb)
    {
        var videosWithMissingThumbnails = videosInDb.ToList().Where(videoInfo =>
                !Directory.Exists(Path.Combine(GlobalVariables.ThumbnailsFolder, videoInfo.Id.ToString())) ||
                !File.Exists(
                    Path.Combine(GlobalVariables.ThumbnailsFolder, videoInfo.Id.ToString(), GlobalVariables.SmallThumbnailFileName)) ||
                !File.Exists(
                    Path.Combine(GlobalVariables.ThumbnailsFolder, videoInfo.Id.ToString(), GlobalVariables.LargeThumbnailFileName)))
            .ToList();

        foreach (var missingThumbnail in videosWithMissingThumbnails)
        {
            await _ffMpegService.GenerateThumbnail(missingThumbnail);
        }
    }

    private void CheckThumbnailFolder(List<VideoInfo> videosInDb)
    {
        var thumbnailFolders = Directory.GetDirectories(GlobalVariables.ThumbnailsFolder).ToList();

        var wrongFolders =
            thumbnailFolders.Where(folder =>
                videosInDb.ToList().All(videoInfo => videoInfo.Id.ToString() != Path.GetFileName(folder))).ToList();
        
        DeleteThumbnailsFolder(wrongFolders);
    }

    private async Task CheckNewVideos(List<string> videosInFolder, List<VideoInfo> videosInDb)
    {
        var newVideosPaths =
            videosInFolder.Where(folderVideo => videosInDb.All(dbVideo => dbVideo.FilePath != folderVideo)).ToList();

        await ProcessNewVideos(newVideosPaths);
    }

    private void CheckMissingVideos(List<VideoInfo> videosInDb, List<string> videosInFolder)
    {
        var missingVideos =
            videosInDb.Where(dbVideo => videosInFolder.All(folderVideo => folderVideo != dbVideo.FilePath)).ToList();

        DeleteMissingVideos(missingVideos);
    }

    private void DeleteThumbnailsFolder(List<string> wrongFolders)
    {
        foreach (var wrongFolder in wrongFolders)
        {
            if (!Directory.Exists(wrongFolder)) return;
            Directory.Delete(wrongFolder, true);
        }
    }

    private void DeleteMissingVideos(List<VideoInfo> missingVideos)
    {
        foreach (var video in missingVideos)
        {
            _repository.DeleteVideo(video.Id);
        }
    }

    private async Task ProcessNewVideos(List<string> newVideosPaths)
    {
        foreach (var videosPath in newVideosPaths)
        {
            var videoInfo = await GenerateVideoInfo(videosPath);

            await _ffMpegService.GenerateThumbnail(videoInfo);

            _repository.AddNewVideo(videoInfo);
        }
    }
    
    private async Task<VideoInfo> GenerateVideoInfo(string filePath)
    {
        var id = Guid.NewGuid();
        await _ffMpegService.CheckFfmpegFiles();
        var mediaInfo = await FFmpeg.GetMediaInfo(filePath);
        var duration = mediaInfo.Duration.TotalSeconds;

        return new VideoInfo
        {
            Id = id,
            FilePath = filePath,
            Duration = duration,
            IsLiked = false
        };
    }
}