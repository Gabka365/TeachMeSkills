using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Game;

namespace PortalAboutEverything.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            var second = DateTime.Now.Second;
            var dayName = DateTime.Now.DayOfWeek.ToString();
            
            var days = Enum.GetNames<DayOfWeek>().ToList();

            var viewModel = new GameIndexViewModel
            {
                Second = second,
                DayName = dayName,
                Days = days
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult RateTheGame(GameRateViewModel rateViewModel)
        {
            return View(rateViewModel);
        }
    }
}
