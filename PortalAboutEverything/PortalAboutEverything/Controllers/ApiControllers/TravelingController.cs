using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Services;


namespace PortalAboutEverything.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class TravelingController : Controller
    {
        private TravelingRepositories _travelingRepositories;
        private PathHelper _pathHelper;
        private readonly string[] _validExtensions = new[] { "png", "jpg", "jpeg", "gif" };

        public TravelingController(TravelingRepositories travelingRepositories, PathHelper pathHelper)                                  
        {
            _travelingRepositories = travelingRepositories;
            _pathHelper = pathHelper;
           
        }

        public void DeletePost(int postId)
        {
            foreach (var ext in _validExtensions)
            {
                var imageName = $"{postId}.{ext}";
                var folderPatch = _pathHelper.GetPathToTravelingImageFolder();
                var imagePath = Path.Combine(folderPatch, imageName);
           
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                    break;
                }
            }
            _travelingRepositories.Delete(postId);          
        }
    }
}
