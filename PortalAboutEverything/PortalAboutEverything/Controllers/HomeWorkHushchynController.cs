using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Hushchyn;

namespace PortalAboutEverything.Controllers
{
    public class HomeWorkHushchynController : Controller
    {
        public IActionResult Index()
        {
            var second = DateTime.Now.Second;

            var random = new Random();

            var days = Enum.GetNames<DayOfWeek>().ToList();

            var viewModel = new HomeWorkHushchynViewModel
            {
                Second = second,
                Number = random.Next(1, 100),
                Days = days,
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Page2(Page2VIewModel page2ViewModel)
        {
            return View(page2ViewModel);
        }
    }
}
