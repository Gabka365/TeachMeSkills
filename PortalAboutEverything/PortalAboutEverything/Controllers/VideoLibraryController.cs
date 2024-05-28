using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model.VideoLibrary;
using PortalAboutEverything.Data.Repositories.VideoLibrary;
using PortalAboutEverything.Models.VideoLibrary;
using PortalAboutEverything.VideoServices.Services;
using ThumbnailSizeEnum = PortalAboutEverything.VideoServices.Enums.ThumbnailSizeEnum;

namespace PortalAboutEverything.Controllers;

public class VideoLibraryController : Controller
{
    private readonly VideoFileSystemService _videoFileSystemService;
    private readonly VideoRepository _videoRepository;

    public VideoLibraryController(VideoRepository videoRepository, VideoFileSystemService videoFileSystemService)
    {
        _videoRepository = videoRepository;
        _videoFileSystemService = videoFileSystemService;
    }

    [Authorize]
    [HttpGet]
    public IActionResult Index()
    {
        var videos = _videoRepository.GetAllWithFolders(false).Select(GenerateVideoViewModel).ToList();

        return View(videos);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Liked()
    {
        var videos = _videoRepository.GetAllWithFolders(true).Select(GenerateVideoViewModel).ToList();

        return View(videos);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Player(int id)
    {
        var video = _videoRepository.Get(id);

        if (video is null)
        {
            return BadRequest("Видео с таким Id не найдено");
        }

        return View(GenerateVideoViewModel(video));
    }

    [HttpGet]
    public IActionResult Thumbnail(int id, [FromQuery] string size)
    {
        var isSuccess = Enum.TryParse(size, out ThumbnailSizeEnum parsedSize);

        if (!isSuccess)
        {
            return BadRequest("Wrong size");
        }

        var thumbnailPath = _videoFileSystemService.GetThumbnailPath(id, parsedSize);

        return System.IO.File.Exists(thumbnailPath)
            ? Ok(System.IO.File.OpenRead(thumbnailPath))
            : NotFound("Thumbnail file not found");
    }

    [Authorize]
    [HttpGet]
    public void UpdateLibrary()
    {
        _videoFileSystemService.StartUpdateLibrary();
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetVideo(int id)
    {
        var videoPath = _videoRepository.GetVideoPath(id);

        if (videoPath is null)
        {
            return BadRequest("Видео с таким Id не найдено");
        }

        var videoStream = System.IO.File.OpenRead(videoPath);

        return File(videoStream, "video/mp4", Request.Headers.ContainsKey("Range"));
    }

    [Authorize]
    [HttpGet]
    public int? GetRandomVideoId([FromQuery] bool isLiked)
    {
        return _videoRepository.GetRandomVideoId(isLiked);
    }

    [Authorize]
    [HttpGet]
    public void UpdateVideoLikeState(int id, [FromQuery] bool isLiked)
    {
        _videoRepository.UpdateLikeState(id, isLiked);
    }

    [Authorize]
    [HttpGet]
    public void DeleteVideo(int id)
    {
        _ = _videoFileSystemService.DeleteVideo(id);
    }

    private VideoLibraryVideoViewModel GenerateVideoViewModel(Video video)
    {
        return new VideoLibraryVideoViewModel
        {
            Id = video.Id,
            FileName = Path.GetFileNameWithoutExtension(video.FilePath),
            FileFolder = Path.GetFileName(Path.GetDirectoryName(video.FilePath))!,
            VideoDuration = TimeSpan.FromSeconds(video.Duration).ToString(@"hh\:mm\:ss")
        };
    }
}