using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Home;
using PortalAboutEverything.Services;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Controllers
{
    public class HomeController : Controller
    {
        private AuthService _authService;
        private HttpChatApiService _httpChatApiService;

        public HomeController(AuthService authService, HttpChatApiService httpChatApiService)
        {
            _authService = authService;
            _httpChatApiService = httpChatApiService;
        }

        public IActionResult Index()
        {
            var a = _httpChatApiService.GetMessageCount();
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
