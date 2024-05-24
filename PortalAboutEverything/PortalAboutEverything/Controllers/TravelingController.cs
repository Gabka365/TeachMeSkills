using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Game;
using PortalAboutEverything.Models.Traveling;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace PortalAboutEverything.Controllers
{
    public class TravelingController : Controller
    {
        private TravelingRepositories _travelingRepositories;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly string _pathTravelingUserPictures;
        private readonly string[] _validExtensions = new[] { "png", "jpg", "jpeg", "gif" };

        public TravelingController(TravelingRepositories travelingRepositories, IWebHostEnvironment hostingEnvironment)
        {
            _travelingRepositories = travelingRepositories;
            _hostingEnvironment = hostingEnvironment;
            _pathTravelingUserPictures = Path.Combine(_hostingEnvironment.WebRootPath, "images", "Traveling", "UserPictures");
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

        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(TravelingCreateViewModel createTravelingViewModel, IFormFile image)
        {
            var traveling = new Traveling
            {
                Name = createTravelingViewModel.Name,
                Desc = createTravelingViewModel.Desc,
                TimeOfCreation = createTravelingViewModel.TimeOfCreation,
            };
            _travelingRepositories.Create(traveling);
            if (image != null)
            {
                var fileExt = Path.GetExtension(Path.GetFileName(image.FileName)).Substring(1);
                var imageName = $"{traveling.Id}.{fileExt}";
                if (_validExtensions.Contains(fileExt))
                {
                    if (!Directory.Exists(_pathTravelingUserPictures))
                    {
                        Directory.CreateDirectory(_pathTravelingUserPictures);
                    }

                    var path = Path.Combine(_pathTravelingUserPictures, imageName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
                }
            }
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
           };

    }
}
