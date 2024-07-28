using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Services.Apis;
using PortalAboutEverything.Services.AuthStuff.Interfaces;

namespace PortalAboutEverything.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    [Authorize]
    public class BoardGameReviewController : Controller
    {
        private readonly IAuthService _authServise;
        private readonly HttpBoardGamesReviewsApiService _apiServise;

        public BoardGameReviewController(IAuthService authServise, HttpBoardGamesReviewsApiService apiServise)
        {
            _authServise = authServise;
            _apiServise = apiServise;
        }

        public async Task<bool> Delete(int id)
        {
            var review = await _apiServise.GetReviewAsync(id);

            if (review.UserId != _authServise.GetUserId() && !_authServise.HasPermission(Permission.CanModerateReviewsOfBoardGames))
            {
                return false;
            }

            await _apiServise.DeleteReviewAsync(id);
            return true;
        }
    }
}
