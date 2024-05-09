using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Repositories;
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
            var month = DateTime.Now.ToString("MMMM", new System.Globalization.CultureInfo("en-US"));
            var year = DateTime.Now.Year;
            var day = DateTime.Now.Day;

            var model = new TravelingIndexViewModel
            {
                Month = month,
                Year = year,
                Day = day,
                When = $"{month} {day} {year}"
            };

            return View(model);
        }

        public IActionResult Post()
        {
            return View();
        }

      
        //[HttpPost]
        //public IActionResult Post()
        //{


        //    return View();
        //}
    }
}
