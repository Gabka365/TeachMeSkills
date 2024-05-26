using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Auth;
using System.Security.Claims;

namespace PortalAboutEverything.Controllers
{
    public class AuthController : Controller
    {
        public const string AUTH_METHOD = "Smile";

        private UserRepository _userRepository;

        public AuthController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AuthViewModel model)
        {
            var user = _userRepository.GetByLoginAndPasswrod(model.Login, model.Password);
            if (user == null)
            {
                return View(model);
            }

            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.UserName),
                new Claim(ClaimTypes.AuthenticationMethod,AUTH_METHOD)
            };

            var identity = new ClaimsIdentity(claims, AUTH_METHOD);

            var principal = new ClaimsPrincipal(identity);

            HttpContext
                .SignInAsync(principal)
                .Wait();

            return Redirect("/");
        }
    
        public IActionResult Logout()
        {
            HttpContext
                .SignOutAsync()
                .Wait();

            return Redirect("/");
        }
    }
}
