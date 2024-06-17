using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.User;

namespace PortalAboutEverything.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private UserRepository _userRepository;

        public AuthController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsLoginAvailable(string login)
        {
            // throw new Exception("Bad");
            return !_userRepository.Exist(login);
        }

        public UserPermissionViewModel GetUserInfo(int userId)
        {
            var user = _userRepository.Get(userId);
            var viewModel = new UserPermissionViewModel
            {
                Id = userId,
                Name = user.UserName,
                Permission = user.Permission
            };
            return viewModel;
        }
    }
}
