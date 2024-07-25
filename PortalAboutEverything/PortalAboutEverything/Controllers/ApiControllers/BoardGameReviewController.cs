using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Services;
using PortalAboutEverything.Models.BoardGame;
using PortalAboutEverything.Mappers;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Services.AuthStuff;
using PortalAboutEverything.Data.Repositories.Interfaces;
using PortalAboutEverything.Services.Apis;

namespace PortalAboutEverything.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    [Authorize]
    public class BoardGameReviewController : Controller
    {
        private readonly AuthService _authServise;
        private readonly HttpBoardGamesReviewsApiService _apiServise;

        public BoardGameReviewController(AuthService authServise, HttpBoardGamesReviewsApiService apiServise)
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
