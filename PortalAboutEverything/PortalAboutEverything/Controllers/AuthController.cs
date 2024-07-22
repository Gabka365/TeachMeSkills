using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Auth;
using PortalAboutEverything.Services.AuthStuff;
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

            LoginUser(user);

            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegisrationViewModel viewModel)
        {
            if (_userRepository.Exist(viewModel.Login))
            {
                ModelState.AddModelError(nameof(RegisrationViewModel.Login), "Такой пользователь уже есть");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new User
            {
                UserName = viewModel.Login,
                Password = viewModel.Password,
                Role = UserRole.User,
            };

            _userRepository.Create(user);

            LoginUser(user);

            return Redirect("/");
        }

        private void LoginUser(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(AuthClaimsConstants.ID, user.Id.ToString()),
                new Claim(AuthClaimsConstants.NAME, user.UserName),
                new Claim(AuthClaimsConstants.ROLE, user.Role.ToString()),
                new Claim(AuthClaimsConstants.PERMISSION, user.Permission.ToString()),
                new Claim(AuthClaimsConstants.LANGUAGE, user.Language.ToString()),
                new Claim(ClaimTypes.AuthenticationMethod,AUTH_METHOD)
            };

            var identity = new ClaimsIdentity(claims, AUTH_METHOD);

            var principal = new ClaimsPrincipal(identity);

            HttpContext
                .SignInAsync(principal)
                .Wait();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext
                .SignOutAsync();
            
            return Redirect("/");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
