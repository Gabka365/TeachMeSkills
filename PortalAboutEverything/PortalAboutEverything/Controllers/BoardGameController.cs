using Microsoft.AspNetCore.Mvc;

namespace PortalAboutEverything.Controllers
{
    public class BoardGameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FirstBoardGame()
        {
            return View();
        }
    }
}
