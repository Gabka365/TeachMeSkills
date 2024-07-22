using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Mappers;
using PortalAboutEverything.Models.BoardGameReview;
using PortalAboutEverything.Services;
using BoardGamesReviewsApi.Dtos;

namespace PortalAboutEverything.Controllers
{
    [Authorize]
    public class BoardGameReviewController : Controller
    {
        private readonly BoardGameRepositories _gameRepositories;
        private readonly BoardGameMapper _mapper;
        private readonly HttpBoardGamesReviewsApiService _httpService;

        public BoardGameReviewController(BoardGameRepositories gameRepositories,
            BoardGameMapper mapper,
            HttpBoardGamesReviewsApiService httpService)
        {
            _gameRepositories = gameRepositories;
            _mapper = mapper;
            _httpService = httpService;
        }

        [HttpGet]
        public IActionResult Create(int gameId)
        {
            BoardGameCreateReviewViewModel createReviewViewModel =
            new BoardGameCreateReviewViewModel
                {
                    BoardGameId = gameId,
                    BoardGameName = _gameRepositories.Get(gameId)!.Title,
                };

            return View(createReviewViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BoardGameCreateReviewViewModel boardGameReviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameReviewViewModel);
            }

            DtoBoardGameReviewCreate review = _mapper.BuildBoardGameRewievDataModelFromCreate(boardGameReviewViewModel);
            review.BoardGameId = boardGameReviewViewModel.BoardGameId;
            await _httpService.CreateReviewAsync(review);

            return RedirectToAction("BoardGame", "BoardGame", new { id = review.BoardGameId });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id, int gameId)
        {
            DtoBoardGameReview reviewForUpdate = await _httpService.GetReviewAsync(id);
            BoardGameUpdateReviewViewModel viewModel = _mapper.BuildBoardGameUpdateRewievViewModel(reviewForUpdate);
            viewModel.BoardGameId = gameId;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(BoardGameUpdateReviewViewModel boardGameReviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameReviewViewModel);
            }

            DtoBoardGameReviewUpdate updatedReview = _mapper.BuildBoardGameRewievDataModelFromUpdate(boardGameReviewViewModel);
            await _httpService.UpdateReviewAsync(updatedReview);

            return RedirectToAction("BoardGame", "BoardGame", new { id = boardGameReviewViewModel.BoardGameId });
        }     
    }
}
