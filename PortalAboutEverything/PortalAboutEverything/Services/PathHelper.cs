using Microsoft.AspNetCore.Hosting;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Services
{
    public class PathHelper
    {
        private IWebHostEnvironment _webHostEnvironment;

        public PathHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetPathToGameCover(int gameId)
        {
            var fileName = $"cover-{gameId}.jpg";
            return GetPathByFolder("images\\Game", fileName);
        }

        public bool IsGameCoverExist(int id)
        {
            var path = GetPathToGameCover(id);
            return File.Exists(path);
        }

        public string GetPathToBoardGameMainImage(int boardGameId)
        {
            var fileName = $"mainImage-{boardGameId}.jpg";
            return GetPathByFolder("images\\BoardGame", fileName);
        }

        public bool IsBoardGameMainImageExist(int id)
        {
            var path = GetPathToBoardGameMainImage(id);
            return File.Exists(path);
        }

        public string GetPathToBoardGameSideImage(int boardGameId)
        {
            var fileName = $"sideImage-{boardGameId}.jpg";
            return GetPathByFolder("images\\BoardGame", fileName);
        }

        public bool IsBoardGameSideImageExist(int id)
        {
            var path = GetPathToBoardGameSideImage(id);
            return File.Exists(path);
        }

        private string GetPathByFolder(string pathToFolder, string fileName)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, pathToFolder, fileName);
            return path;
        }
    }
}
