﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Mappers;
using PortalAboutEverything.Models.BoardGameReview;
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
        private readonly BoardGameMapper _mapper;

        public BoardGameReviewController(IAuthService authServise, 
            HttpBoardGamesReviewsApiService apiServise, 
            BoardGameMapper mapper)
        {
            _authServise = authServise;
            _apiServise = apiServise;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public async Task<List<BoardGameReviewViewModel>> GelAllForBoardGame(int gameId)
        {
            User user = null;
            bool isModerator = false; ;

            if (_authServise.IsAuthenticated())
            {
                user = _authServise.GetUser();
                isModerator = _authServise.HasPermission(Permission.CanModerateReviewsOfBoardGames);
            }

            var reviewsDto = await _apiServise.GetAllReviewsForGameAsync(gameId);
            var dtos = reviewsDto
                .Select((dto) => _mapper.BuildBoardGameReviewViewModel(dto, user, isModerator))
                .ToList();
            return dtos;
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
