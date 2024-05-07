using Microsoft.AspNetCore.Mvc;

namespace PortalAboutEverything.Controllers
{
    public class VertukController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Post()
        {
            return View();
        }
    }
}
