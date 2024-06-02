using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Game;
using PortalAboutEverything.Models.Traveling;
using PortalAboutEverything.Services;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Linq;

namespace PortalAboutEverything.Controllers
{
    public class TravelingController : Controller
    {
        private TravelingRepositories _travelingRepositories;
        private UserRepository _userRepository;
        private IWebHostEnvironment _hostingEnvironment;
        private AuthService _authService;
        private readonly string _pathTravelingUserPictures;
        private readonly string _pathTravelingIndexPictures;
        private readonly CommentRepository _commentRepository;
        private readonly string[] _validExtensions = new[] { "png", "jpg", "jpeg", "gif" };

        public TravelingController(TravelingRepositories travelingRepositories, IWebHostEnvironment hostingEnvironment,
                                   UserRepository userRepository, AuthService authService, CommentRepository commentRepository)
        {
            _travelingRepositories = travelingRepositories;
            _hostingEnvironment = hostingEnvironment;
            _pathTravelingUserPictures = Path.Combine(_hostingEnvironment.WebRootPath, "images", "Traveling", "UserPictures");
            _pathTravelingIndexPictures = Path.Combine(_hostingEnvironment.WebRootPath, "images", "Traveling");
            _userRepository = userRepository;
            _authService = authService;
            _commentRepository = commentRepository;
        }

        public IActionResult Index()
        {
            var dateTime1 = new DateTime(2015, 9, 25);
            var dateTime2 = new DateTime(2016, 10, 13);
            var dateTime3 = new DateTime(2012, 1, 14);
            var dateTime4 = new DateTime(2014, 5, 20);
            var dateTime5 = new DateTime(2011, 4, 22);
            var dateTime6 = new DateTime(2010, 2, 21);

            var model = new TravelingIndexViewModel();
            model.TravelingDate.Add(dateTime1);
            model.TravelingDate.Add(dateTime2);
            model.TravelingDate.Add(dateTime3);
            model.TravelingDate.Add(dateTime4);
            model.TravelingDate.Add(dateTime5);
            model.TravelingDate.Add(dateTime6);

            return View(model);
        }

        [Authorize]
        [HttpGet]
        [HasRoleOrHigher(UserRole.TravelingAdmin)]
        public IActionResult ChengeIndexPage()
        {
            var images = Directory.EnumerateFiles(Path.Combine(_hostingEnvironment.WebRootPath, "images", "Traveling"))
                                .Select(fn => "~/images/Traveling/" + Path.GetFileName(fn));
            return View(images);
        }
        [Authorize]
        [HttpPost]
        [HasRoleOrHigher(UserRole.TravelingAdmin)]
        public IActionResult ChengeImageIndexPage(string oldImagePath, IFormFile newImage)
        {
            if (newImage != null)
            {
                string fileName = Path.GetFileName(oldImagePath);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

                var fileExt = Path.GetExtension(Path.GetFileName(newImage.FileName)).Substring(1).ToLower();
                var imageName = $"{fileNameWithoutExtension}.{fileExt}";

                var path = Path.Combine(_pathTravelingIndexPictures, imageName);
                System.IO.File.Delete(path);

                if (_validExtensions.Contains(fileExt))
                {
                    SaveImageToDirectory(_pathTravelingIndexPictures, path, newImage);
                }
            }
            return RedirectToAction("ChengeIndexPage");
        }


        [HttpGet]
        public IActionResult TravelingPosts()
        {
            var travelingPostsViewModel = _travelingRepositories
                .GetAll()
                .Select(BuildTravelingShowPostsViewModel)
                .ToList();

            return View(travelingPostsViewModel);
        }
        [HttpGet]
        public IActionResult ShowImage(int id)
        {
            string travelingImage = null;
            foreach (var ext in _validExtensions)
            {
                var imageName = $"{id}.{ext}";
                var imagePath = Path.Combine(_pathTravelingUserPictures, imageName);

                if (System.IO.File.Exists(imagePath))
                {
                    travelingImage = imageName;
                    break;
                }
            }
            return Ok(System.IO.File.OpenRead(Path.Combine(_pathTravelingUserPictures, travelingImage)));
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePost(TravelingCreateViewModel createTravelingViewModel, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return View(createTravelingViewModel);
            }
            var userId = _authService.GetUserId();
            var user = _userRepository.Get(userId);
            var traveling = new Traveling
            {
                Name = createTravelingViewModel.Name,
                Desc = createTravelingViewModel.Desc,
                TimeOfCreation = createTravelingViewModel.TimeOfCreation,
                User = user
            };
            _travelingRepositories.Create(traveling);
            if (image != null)
            {
                var fileExt = Path.GetExtension(Path.GetFileName(image.FileName)).Substring(1).ToLower();
                var imageName = $"{traveling.Id}.{fileExt}";
                var path = Path.Combine(_pathTravelingUserPictures, imageName);
                if (_validExtensions.Contains(fileExt))
                {
                    SaveImageToDirectory(_pathTravelingUserPictures, path, image);
                }
            }
            return RedirectToAction("TravelingPosts");
        }

        [HttpPost]
        public IActionResult CreateComment(int id, TravelingCreateComment travelingCreateComment)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).First().ErrorMessage;

                TempData["ErrorMessage"] = errors;

                return RedirectToAction("TravelingPosts");
            }
            var comment = new Comment
            {
                Text = travelingCreateComment.Text,
                Traveling = _travelingRepositories.Get(id)
            };

            _commentRepository.Create(comment);

            return RedirectToAction("TravelingPosts");
        }

        public IActionResult DeletePost(int id)
        {
            foreach (var ext in _validExtensions)
            {
                var imageName = $"{id}.{ext}";
                var imagePath = Path.Combine(_pathTravelingUserPictures, imageName);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                    break;
                }
            }
            _travelingRepositories.Delete(id);
            return RedirectToAction("TravelingPosts");
        }

        [HttpGet]
        public IActionResult UpdatePost(int id)
        {
            var traveling = _travelingRepositories.Get(id);
            var model = new TravelingUpdateViewModel()
            {
                Id = traveling.Id,
                Name = traveling.Name,
                Desc = traveling.Desc,

            };
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdatePost(TravelingUpdateViewModel model)
        {
            var traveling = new Traveling
            {
                Id = model.Id,
                Name = model.Name,
                Desc = model.Desc,
                TimeOfCreation = DateTime.Now.ToString("dd MMM yyyy"),

            };
            _travelingRepositories.Update(traveling);
            return RedirectToAction("TravelingPosts");
        }

        private TravelingShowPostsViewModel BuildTravelingShowPostsViewModel(Traveling traveling)
           => new TravelingShowPostsViewModel
           {
               Id = traveling.Id,
               Desc = traveling.Desc,
               Name = traveling.Name,
               TimeOfCreation = traveling.TimeOfCreation,
               UserId = traveling.User.Id,
               Comments = _commentRepository.GetWithTravel(traveling.Id).Select(c => new TravelingCreateComment
               {
                   Text = c.Text,

               }).ToList()
           };
        private void SaveImageToDirectory(string directoryPath, string filePath, IFormFile image)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
        }

    }
}
