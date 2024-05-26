using PortalAboutEverything.Data.Enums.VideoLibrary;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Variables.VideoLibrary;

namespace PortalAboutEverything.Data.Repositories;

public class VideoLibraryRepository
{
    private readonly PortalDbContext _dbContext;

    public VideoLibraryRepository(PortalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddNewVideo(VideoInfo videoInfo)
    {
        videoInfo.Id = Guid.NewGuid();

        _dbContext.Videos.Add(videoInfo);
        _dbContext.SaveChanges();
    }

    public List<VideoInfo> GetAllVideos() => _dbContext.Videos.ToList();
    
    public List<VideoInfo> GetAllVideos(bool isLiked) => _dbContext.Videos.Where(videoInfo => videoInfo.IsLiked == isLiked).ToList();

    public VideoInfo GetVideo(Guid id) => _dbContext.Videos.Single(videoInfo => videoInfo.Id == id);

    public void UpdateVideoLikeState(Guid id, bool isLiked)
    {
        var video = _dbContext.Videos.Single(videoInfo => videoInfo.Id == id);
        video.IsLiked = isLiked;

        _dbContext.SaveChanges();
    }

    public void DeleteVideo(Guid id)
    {
        var video = _dbContext.Videos.Single(videoInfo => videoInfo.Id == id);
        
        if(File.Exists(video.FilePath)) File.Delete(video.FilePath);
        
        _dbContext.Videos.Remove(video);

        _dbContext.SaveChanges();
    }

    public string GetThumbnailPath(Guid id, ThumbnailSizeEnum thumbnailSize)
    {
        var video = _dbContext.Videos.Single(videoInfo => videoInfo.Id == id);

        switch (thumbnailSize)
        {
            default:
            case ThumbnailSizeEnum.Small:
                return Path.Combine(GlobalVariables.ThumbnailsFolder, video.Id.ToString(), GlobalVariables.SmallThumbnailFileName);
            case ThumbnailSizeEnum.Large:
                return Path.Combine(GlobalVariables.ThumbnailsFolder, video.Id.ToString(), GlobalVariables.LargeThumbnailFileName);
        }
    }

    public string GetVideoPath(Guid id)
    {
        var video = _dbContext.Videos.Single(videoInfo => videoInfo.Id == id);

        return video.FilePath;
    }

    public string GetRandomVideoId(bool isLiked)
    {
        var video = _dbContext.Videos.Where(video => video.IsLiked == isLiked).ToList();

        return video[new Random().Next(video.Count)].Id.ToString();
    }
}