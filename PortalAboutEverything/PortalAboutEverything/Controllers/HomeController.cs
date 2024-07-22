using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Home;
using PortalAboutEverything.Services.Apis;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Controllers
{
    public class HomeController : Controller
    {
        private AuthService _authService;
        private HttpChatApiService _httpChatApiService;
        private HttpApiKittyService _httpApiKittyService;

        public HomeController(AuthService authService, 
            HttpChatApiService httpChatApiService, 
            HttpApiKittyService httpApiKittyService)
        {
            _authService = authService;
            _httpChatApiService = httpChatApiService;
            _httpApiKittyService = httpApiKittyService;
        }

        public async Task<IActionResult> Index()
        {
            var messageCountTask = _httpChatApiService.GetMessageCountAsync();
            var kittyTask = _httpApiKittyService.GetKittyAsync();

            await Task.WhenAll(messageCountTask, kittyTask);

            var viewModel = new IndexViewModel()
            {
                KittyUrl = kittyTask.Result.url,
                ChatMessageCount = messageCountTask.Result.MessageCount,
            };

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
