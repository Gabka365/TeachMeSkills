using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories;

public class VideoLibraryRepository : Repository<VideoInfo>
{
    private readonly PortalDbContext _dbContext;

    public VideoLibraryRepository(PortalDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Create(VideoInfo videoInfo)
    {
        _dbContext.Videos.Add(videoInfo);
        _dbContext.SaveChanges();
    }

    public List<VideoInfo> GetAll(bool isLiked)
    {
        return _dbContext.Videos.Where(videoInfo => videoInfo.IsLiked == isLiked).ToList();
    }

    public void UpdateLikeState(Guid id, bool isLiked)
    {
        var video = _dbContext.Videos.FirstOrDefault(videoInfo => videoInfo.Id == id);

        if (video is null)
        {
            return;
        }
        
        video.IsLiked = isLiked;

        _dbContext.SaveChanges();
    }

    public override void Delete(Guid id)
    {
        var video = _dbContext.Videos.FirstOrDefault(videoInfo => videoInfo.Id == id);

        if (video is null)
        {
            return;
        }

        _dbContext.Videos.Remove(video);

        _dbContext.SaveChanges();
    }

    public string? GetVideoPath(Guid id)
    {
        return _dbContext.Videos
            .Where(video => video.Id == id)
            .Select(video => video.FilePath)
            .FirstOrDefault();
    }

    public string GetRandomVideoId(bool isLiked)
    {
        var video = _dbContext.Videos
            .Where(video => video.IsLiked == isLiked)
            .Select(video => video.Id).ToList();

        return video[new Random().Next(video.Count)].ToString();
    }
}