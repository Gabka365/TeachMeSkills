using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model.VideoLibrary;

namespace PortalAboutEverything.Data.Repositories.VideoLibrary;

public class FolderRepository : BaseRepository<Folder>
{
    public FolderRepository(PortalDbContext dbContext) : base(dbContext)
    {
    }

    public List<Folder> GetAllWithVideos()
    {
        return _dbSet.Include(folder => folder.Videos).ToList();
    }

    public Folder AddFolder(Folder folder)
    {
        _dbSet.Add(folder);
        _dbContext.SaveChanges();

        return folder;
    }

    public Folder? Get(string folderName)
    {
        return _dbSet.FirstOrDefault(folder => folder.Name == folderName);
    }

    public void DeleteFolders(List<Folder> folders)
    {
        _dbSet.RemoveRange(folders);
        _dbContext.SaveChanges();
    }
}