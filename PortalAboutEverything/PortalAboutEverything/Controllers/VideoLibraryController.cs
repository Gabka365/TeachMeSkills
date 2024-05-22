using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.VideoLibrary;
using PortalAboutEverything.VideoServices.Enums;
using PortalAboutEverything.VideoServices.Services;

namespace PortalAboutEverything.Controllers;

public class VideoLibraryController : Controller
{
    private readonly VideoLibraryRepository _videoLibraryRepository;
    private readonly VideoService _video;

    public VideoLibraryController(VideoLibraryRepository videoLibraryRepository, VideoService video)
    {
        _videoLibraryRepository = videoLibraryRepository;
        _video = video;
    }

    [HttpGet]
    public IActionResult ndex()
    {
        var videos = _videoLibraryRepository.GetAll(isLiked: false).Select(GenerateVideoViewModel).ToList();
        
        return View(videos);
    }

    [HttpGet]
    public IActionResult Liked()
    {
        var videos = _videoLibraryRepository.GetAll(isLiked: true).Select(GenerateVideoViewModel).ToList();
        
        return View(videos);
    }

    [HttpGet]
    public IActionResult Player(Guid id)
    {
        if (string.IsNullOrWhiteSpace(id.ToString())) return BadRequest();
        var video = _videoLibraryRepository.Get(id);

        return video is null ? View() : View(GenerateVideoViewModel(video));
    }

    [HttpGet]
    public IActionResult Thumbnail(Guid id, [FromQuery] string size)
    {
        var isSuccess = Enum.TryParse(size, out ThumbnailSizeEnum parsedSize);

        if (!isSuccess)
        {
            return BadRequest("Wrong size");
        }

        var thumbnailPath = _video.GetThumbnailPath(id, parsedSize);

        return thumbnailPath is not null || System.IO.File.Exists(thumbnailPath)
            ? Ok(System.IO.File.OpenRead(thumbnailPath))
            : NotFound("Thumbnail file not found");
    }

    [HttpGet]
    public void UpdateLibrary()
    {
        _ = _video.ScanUnsortedVideoFolder();
    }

    [HttpGet]
    public IActionResult GetVideo(Guid id)
    {
        var videoPath = _videoLibraryRepository.GetVideoPath(id);

        if (videoPath is null)
        {
            return NotFound("Видео не найдено");
        }

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
        _videoLibraryRepository.UpdateLikeState(id, isLiked);
    }

    [HttpGet]
    public void DeleteVideo(Guid id)
    {
        var video = _videoLibraryRepository.Get(id);

        if (video is null)
        {
            return;
        }
        
        _video.DeleteVideo(video.FilePath);
        VideoService.DeleteThumbnailFolder(id.ToString());
        _videoLibraryRepository.Delete(id);
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