using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Traveling;
using PortalAboutEverything.Services;
using PortalAboutEverything.Services.AuthStuff;
using System.Reflection;



namespace PortalAboutEverything.Controllers
{
    public class TravelingController : Controller
    {
        private TravelingRepositories _travelingRepositories;
        private UserRepository _userRepository;
        private LikeRepositories _likeRepository;
        private IWebHostEnvironment _hostingEnvironment;
        private AuthService _authService;
        private HttpNewsTravelingsApi _httpNewsTravelingsApi;
        private readonly string _pathTravelingUserPictures;
        private readonly string _pathTravelingIndexPictures;
        private readonly CommentRepository _commentRepository;
        private readonly string[] _validExtensions = new[] { "png", "jpg", "jpeg", "gif" };

        public TravelingController(TravelingRepositories travelingRepositories, IWebHostEnvironment hostingEnvironment,
                                   UserRepository userRepository, AuthService authService, CommentRepository commentRepository,
                                    LikeRepositories likeRepository, HttpNewsTravelingsApi httpNewsTravelingsApi)
        {
            _travelingRepositories = travelingRepositories;
            _hostingEnvironment = hostingEnvironment;
            _pathTravelingUserPictures = Path.Combine(_hostingEnvironment.WebRootPath, "images", "Traveling", "UserPictures");
            _pathTravelingIndexPictures = Path.Combine(_hostingEnvironment.WebRootPath, "images", "Traveling");
            _userRepository = userRepository;
            _authService = authService;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
            _httpNewsTravelingsApi = httpNewsTravelingsApi;
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
            model.TravelingDates.Add(dateTime1);
            model.TravelingDates.Add(dateTime2);
            model.TravelingDates.Add(dateTime3);
            model.TravelingDates.Add(dateTime4);
            model.TravelingDates.Add(dateTime5);
            model.TravelingDates.Add(dateTime6);

            model.IsTravingAdmin = User.Identity.IsAuthenticated ? _authService.HasRoleOrHigher(UserRole.TravelingAdmin) : false;

            return View(model);
        }

        [Authorize]
        [HttpGet]
        [HasRoleOrHigher(UserRole.TravelingAdmin)]
        public IActionResult ChengeIndexPage()
        {
            var images = Directory.EnumerateFiles(Path.Combine(_hostingEnvironment.WebRootPath, "images", "Traveling"))
                                  .Select(fn => "~/images/Traveling/" + Path.GetFileName(fn));

            var model = new List<TravelingIndexImageViewModel>();

            foreach (var imageUrl in images)
            {
                var imageName = Path.GetFileName(imageUrl);
                var imageModel = new TravelingIndexImageViewModel
                {
                    ImageName = imageName,
                    ImageUrl = imageUrl
                };
                model.Add(imageModel);
            };

            return View(model);
        }
        [Authorize]
        [HttpPost]
        [HasRoleOrHigher(UserRole.TravelingAdmin)]
        public IActionResult ChengeImageIndexPage(TravelingChengeImageIndexPageViewModel travelingChengeImageIndexPageViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).First().ErrorMessage;

                TempData["ErrorMessage"] = errors;

                return RedirectToAction("ChengeIndexPage");
            }

            string fileName = Path.GetFileName(travelingChengeImageIndexPageViewModel.OldImagePath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            var fileExt = GetFileExtension(travelingChengeImageIndexPageViewModel.NewImage.FileName);
            var imageName = $"{fileNameWithoutExtension}.{fileExt}";

            var path = Path.Combine(_pathTravelingIndexPictures, imageName);

            System.IO.File.Delete(path);
            SaveImageToDirectory(_pathTravelingIndexPictures, path, travelingChengeImageIndexPageViewModel.NewImage);


            return RedirectToAction("ChengeIndexPage");
        }


        [HttpGet]
        public IActionResult TravelingPosts()
        {
            var lastNews = _httpNewsTravelingsApi.GetLastNews();
            var model = new TravelingShowPostsViewModel();

            var travelingPosts = _travelingRepositories
                .GetAll()
                .Select(BuildTravelingShowPostsViewModel)
                .ToList();

            if (travelingPosts.Count != 0)
            {
                var topTraveling = _travelingRepositories.GetTopTreveling();

                var topTravelinViewModel = new TopTravelingByCommentsViewModel
                {
                    Name = topTraveling.Name,
                    Desc = topTraveling.Desc,
                    Id = topTraveling.Id,
                    TimeOfCreation = topTraveling.TimeOfCreation,
                    UserId = topTraveling.UserId,
                    CommentCount = topTraveling.CommentCount,
                    Comments = _commentRepository.GetWithTravel(topTraveling.Id).Select(c => new TravelingCreateComment
                    {
                        Text = c.Text,

                    }).ToList(),
                    countLike = _travelingRepositories.CountLike(topTraveling.Id)

                };

                foreach (var post in travelingPosts)
                {
                    if (post.Id == topTravelinViewModel.Id)
                    {
                        travelingPosts.Remove(post);
                        break;
                    }
                }
                model.TopTravelingByCommentsViewModel = topTravelinViewModel;
            }

            model.TravelingPostsViewModels = travelingPosts;
            model.IsTravingAdmin = User.Identity.IsAuthenticated ? _authService.HasRoleOrHigher(UserRole.TravelingAdmin) : false;
            model.LastNews = lastNews.Text;
            return View(model);
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
        public IActionResult CreatePost(TravelingCreateViewModel createTravelingViewModel)
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

            var fileExt = GetFileExtension(createTravelingViewModel.Image.FileName);
            var imageName = $"{traveling.Id}.{fileExt}";
            var path = Path.Combine(_pathTravelingUserPictures, imageName);

            SaveImageToDirectory(_pathTravelingUserPictures, path, createTravelingViewModel.Image);

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

        public IActionResult TravelingApiInfo()
        {
            var travelingApi = new PortalAboutEverything.Controllers.ApiControllers.TravelingController();
            var typeTravelingApi = travelingApi.GetType();

            var customMethods = typeTravelingApi
                        .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                        .Where(m => m.DeclaringType == typeTravelingApi);
         
            var model = new MethodInfoViewModel();
            var modelList = new List<MethodInfoViewModel>();

            foreach (var method in customMethods)
            {
                var parameters = method.GetParameters();
                string infoparameter = "";
                foreach (var parameter in parameters)
                {
                    infoparameter += $"Имя {parameter.Name} тип {parameter.ParameterType}<br/>";
                }
                model.MethodInfo = ($"Название метода {method.Name},<br/>" +
                            $"Он вернет параметр {method.ReturnParameter.Name} типа {method.ReturnType},<br/>" +
                            $"Входящие параметры:<br/>{infoparameter}<br/>");
                modelList.Add(model);
            }

            return View(modelList);
        }

        private TravelingPostsViewModel BuildTravelingShowPostsViewModel(Traveling traveling)
           => new TravelingPostsViewModel
           {
               Id = traveling.Id,
               Desc = traveling.Desc,
               Name = traveling.Name,
               TimeOfCreation = traveling.TimeOfCreation,
               UserId = traveling.User.Id,
               Comments = _commentRepository.GetWithTravel(traveling.Id).Select(c => new TravelingCreateComment
               {
                   Text = c.Text,

               }).ToList(),
               countLike = _travelingRepositories.CountLike(traveling.Id)

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
        private string GetFileExtension(string fileName)
        {
            return Path.GetExtension(Path.GetFileName(fileName)).Substring(1).ToLower();
        }

    }
}
