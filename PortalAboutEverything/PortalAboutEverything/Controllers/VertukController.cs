using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Game;
using PortalAboutEverything.Models.Vertuk;
using PortalAboutEverything.Services.Vertuk;

namespace PortalAboutEverything.Controllers
{
    public class VertukController : Controller
    {
        private readonly PostsService _postsService;

        public VertukController(PostsService postsService)
        {
            _postsService = postsService;
        }
        public IActionResult Index()
        {
            //создаем модель для отображение месяца года и дня
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
            return View(_postsService.Posts);
        }

        [HttpPost]
        public IActionResult Post(VertukPostViewModel rateViewModel)
        {

            _postsService.Posts.ListPosts.Add(rateViewModel);
            return View(_postsService.Posts);
        }
    }
}
