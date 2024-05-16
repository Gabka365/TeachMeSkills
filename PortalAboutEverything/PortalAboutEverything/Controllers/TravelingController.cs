using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Game;
using PortalAboutEverything.Models.Traveling;


namespace PortalAboutEverything.Controllers
{
    public class TravelingController : Controller
    {
        private TravelingRepositories _travelingRepositories;

        public TravelingController(TravelingRepositories travelingRepositories)
        {
            _travelingRepositories = travelingRepositories;
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
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(TravelingCreateViewModel createTravelingViewModel)
        {

            var traveling = new Traveling
            {
                Name = createTravelingViewModel.Name,
                Desc = createTravelingViewModel.Desc,
                TimeOfCreation = createTravelingViewModel.TimeOfCreation,

            };

            _travelingRepositories.Create(traveling);

            return RedirectToAction("TravelingPosts");
        }

        public IActionResult DeletePost(int id)
        {
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
