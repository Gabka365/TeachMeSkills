using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Enums.VideoLibrary;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Data.Services.VideoLibrary;
using PortalAboutEverything.Models.VideoLibrary;

namespace PortalAboutEverything.Controllers;

public class VideoLibraryController : Controller
{
    private readonly VideoLibraryRepository _videoLibraryRepository;
    private readonly VideoProcessorService _videoProcessor;

    public VideoLibraryController(VideoLibraryRepository videoLibraryRepository, VideoProcessorService videoProcessor)
    {
        _videoLibraryRepository = videoLibraryRepository;
        _videoProcessor = videoProcessor;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var videos = _videoLibraryRepository.GetAllVideos(isLiked: false).Select(GenerateVideoViewModel).ToList();
        
        return View(videos);
    }

    [HttpGet]
    public IActionResult Liked()
    {
        var videos = _videoLibraryRepository.GetAllVideos(isLiked: true).Select(GenerateVideoViewModel).ToList();
        
        return View(videos);
    }

    [HttpGet]
    public IActionResult Player(Guid id)
    {
        if (string.IsNullOrWhiteSpace(id.ToString())) return BadRequest();
        var video = _videoLibraryRepository.GetVideo(id);

        return View(GenerateVideoViewModel(video));
    }

    [HttpGet]
    public IActionResult Thumbnail(Guid id, [FromQuery] string size)
    {
        var isSuccess = Enum.TryParse(size, out ThumbnailSizeEnum parsedSize);

        if (!isSuccess)
        {
            return BadRequest("Wrong size");
        }

        var thumbnailPath = _videoLibraryRepository.GetThumbnailPath(id, parsedSize);

        return System.IO.File.Exists(thumbnailPath)
            ? Ok(System.IO.File.OpenRead(thumbnailPath))
            : NotFound("Thumbnail file not found");
    }

    [HttpGet]
    public async Task UpdateLibrary()
    {
        await _videoProcessor.ScanUnsortedVideoFolder();
    }

    [HttpGet]
    public IActionResult GetVideo(Guid id)
    {
        var videoPath = _videoLibraryRepository.GetVideoPath(id);

        var videoStream = System.IO.File.OpenRead(videoPath);

        return File(videoStream, "video/mp4", Request.Headers.ContainsKey("Range"));
    }

    [HttpGet]
    public string GetRandomVideoId([FromQuery] bool isLiked)
    {
        return _videoLibraryRepository.GetRandomVideoId(isLiked);
    }

    [HttpGet]
    public void UpdateVideoLikeState(Guid id, [FromQuery] bool isLiked)
    {
        _videoLibraryRepository.UpdateVideoLikeState(id, isLiked);
    }

    [HttpGet]
    public void DeleteVideo(Guid id)
    {
        _videoLibraryRepository.DeleteVideo(id);
    }

    private VideoLibraryVideoViewModel GenerateVideoViewModel(VideoInfo videoInfo)
    {
        return new VideoLibraryVideoViewModel
        {
            Id = videoInfo.Id,
            FileName = Path.GetFileNameWithoutExtension(videoInfo.FilePath),
            FileFolder = Path.GetFileName(Path.GetDirectoryName(videoInfo.FilePath))!,
            VideoDuration = TimeSpan.FromSeconds(videoInfo.Duration).ToString(@"hh\:mm\:ss")
        };
    }
}