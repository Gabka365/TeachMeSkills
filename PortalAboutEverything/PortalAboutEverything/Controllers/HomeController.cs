using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Home;
using PortalAboutEverything.Services;

namespace PortalAboutEverything.Controllers
{
    public class HomeController : Controller
    {
        private AuthService _authService;

        public HomeController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            if (_authService.IsAuthenticated())
            {
                viewModel.UserName = _authService.GetUserName();
            }
            else
            {
                viewModel.UserName = "Гость";
            }

            return View(viewModel);
        }
    }
}
