using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Services;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Services.AuthStuff;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;

namespace PortalAboutEverything.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class StoreController : Controller
    {
        private StoreRepositories _storeRepositories;

        private PathHelper _pathHelper;

        private AuthService _authService;
        public StoreController(StoreRepositories storeRepositories, PathHelper pathHelper, AuthService authService)
        {
            _storeRepositories = storeRepositories;
            _pathHelper = pathHelper;
            _authService = authService;
        }

        public class AddToFavouriteRequest
        {
            public int Id { get; set; }
        }

        [HasRoleOrHigher(UserRole.StoreAdmin)]
        [HttpDelete]
        public ActionResult<object> DeleteGood(int id)
        {
            var model = _storeRepositories.GetGoodByIdWithReview(id);
            if (model != null)
            {
                _storeRepositories.Delete(model);

                var path = _pathHelper.GetPathToGoodCover(id);
                System.IO.File.Delete(path);

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public JsonResult AddToFavourite([FromBody] AddToFavouriteRequest request)
        {
            var user = _authService.GetUser();

            var alreadyHaveALike = _storeRepositories.AddUserWhoLikedTheGoodToGood(request.Id, user);
            if (alreadyHaveALike)
            {
                return Json(new { success = true, isLiked = true });
            }
            else
            {
                return Json(new { success = true, isLiked = false });
            }
        }
    }
}
