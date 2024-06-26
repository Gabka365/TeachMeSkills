using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model.VideoLibrary;
using PortalAboutEverything.Data.Repositories.DataModel;
using PortalAboutEverything.Data.Repositories.RawSql;

namespace PortalAboutEverything.Data.Repositories.VideoLibrary;

public class VideoRepository : BaseRepository<Video>
{
    public VideoRepository(PortalDbContext dbContext) : base(dbContext)
    {
    }

    public override List<Video> GetAll()
    {
        return _dbSet.Include(video => video.Folder).ToList();
    }

    public string? GetVideoPath(int id)
    {
        return _dbSet
            .Where(video => video.Id == id)
            .Select(video => video.FilePath)
            .FirstOrDefault();
    }

    public List<Video> GetAllWithFolders(bool isLiked)
    {
        return _dbSet.Include(video => video.Folder).Where(video => video.IsLiked == isLiked).ToList();
    }

    public void UpdateLikeState(int id, bool isLiked)
    {
        var video = _dbSet.FirstOrDefault(videoInfo => videoInfo.Id == id);

        if (video is null)
        {
            throw new KeyNotFoundException();
        }

        video.IsLiked = isLiked;

        _dbContext.SaveChanges();
    }

    // public int? GetRandomVideoId(bool isLiked)
    // {
    //     return _dbSet
    //         .Include(video => video.Folder)
    //         .Where(video => video.IsLiked == isLiked)
    //         .OrderBy(video => Guid.NewGuid())
    //         .Select(video => video.Id)
    //         .FirstOrDefault();
    // }

    public RandomVideoIdDataModel? GetRandomVideoId(bool isLiked)
    {
        return CustomSqlQuery<RandomVideoIdDataModel>(SqlQueryManager.GetRandomVideoId)
            .FirstOrDefault();
    }

    public void DeleteMany(List<Video> videoList)
    {
        _dbSet.RemoveRange(videoList);
        _dbContext.SaveChanges();
    }
}