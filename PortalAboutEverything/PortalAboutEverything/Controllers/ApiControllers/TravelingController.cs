using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Traveling;
using PortalAboutEverything.Services;
using PortalAboutEverything.Services.AuthStuff;


namespace PortalAboutEverything.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class TravelingController : Controller
    {
        private const string BASE_API_URL = "http://localhost:5213/";
        private UserRepository _userRepository;
        private TravelingRepositories _travelingRepositories;
        private LikeRepositories _likeRepositories;
        private PathHelper _pathHelper;
        private AuthService _authService;
        private readonly string[] _validExtensions = new[] { "png", "jpg", "jpeg", "gif" };

        public TravelingController() { }

        public TravelingController(TravelingRepositories travelingRepositories, PathHelper pathHelper, LikeRepositories likeRepositories, AuthService authService, UserRepository userRepository)
        {
            _travelingRepositories = travelingRepositories;
            _pathHelper = pathHelper;
            _likeRepositories = likeRepositories;
            _authService = authService;
            _userRepository = userRepository;
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

        public int LikePost(int postId)
        {

            var userId = _authService.GetUserId();

            if (!_likeRepositories.CheckLikeUserOnTravelingPost(userId, postId))
            {
                var like = new Like();
                _likeRepositories.CreateLikeForTraveling(postId, like, userId);
            }
            else
            {
                var like = _likeRepositories.GetLikeByTravelingPostId(postId, userId);
                _likeRepositories.Delete(like);
            }

            var likeCount = _travelingRepositories.CountLike(postId);

            return likeCount;
        }

        public List<TravelingApiViewModel> GetAll()
        {
            var travelingModel = _travelingRepositories.GetAll()
                                                       .Select(BuildTravelingApiViewModel)
                                                       .ToList();

            return travelingModel;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTravelings(TravelingCreateViewModel travelingCreate)
        {
            if (travelingCreate.Image != null && travelingCreate.Image.Length > 0)
            {
                var user = _userRepository.GetStandartUser();

                var traveling = new Traveling
                {
                    Name = travelingCreate.Name,
                    Desc = travelingCreate.Desc,
                    TimeOfCreation = travelingCreate.TimeOfCreation,
                    User = user,

                };
                _travelingRepositories.Create(traveling);

                var fileExt = GetFileExtension(travelingCreate.Image.FileName);
                var imageName = $"{traveling.Id}.{fileExt}";
                var path = Path.Combine(_pathHelper.GetPathToTravelingImageFolder(), imageName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await travelingCreate.Image.CopyToAsync(stream);
                }

                return Ok(new { message = "Upload successful" });
            }
            return BadRequest("Invalid data");
        }


        private TravelingApiViewModel BuildTravelingApiViewModel(Traveling traveling)
        {
            return new TravelingApiViewModel
            {
                Id = traveling.Id,
                Desc = traveling.Desc,
                Name = traveling.Name
            };
        }
        private string GetFileExtension(string fileName)
        {
            return Path.GetExtension(Path.GetFileName(fileName)).Substring(1).ToLower();
        }
    }
}
