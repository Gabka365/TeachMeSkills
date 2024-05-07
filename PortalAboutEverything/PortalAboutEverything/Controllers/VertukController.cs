using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Game;
using PortalAboutEverything.Models.Vertuk;

namespace PortalAboutEverything.Controllers
{
    public class VertukController : Controller
    {
        private VertukListPostsViewModel _listPost = new VertukListPostsViewModel();
        public IActionResult Index()
        {
            // создаем модель для отображение месяца года и дня
            var month = DateTime.Now.ToString("MMMM", new System.Globalization.CultureInfo("en-US"));
            var year = DateTime.Now.Year;
            var day = DateTime.Now.Day;

            var model = new VertukIndexViewModel
            {
                Month = month,
                Year = year,
                Day = day
            };

            return View(model);
        }

        public IActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post(VertukPostViewModel rateViewModel)
        {
            _listPost.listPosts.Add(rateViewModel);
            return View(_listPost);
        }
    }
}
