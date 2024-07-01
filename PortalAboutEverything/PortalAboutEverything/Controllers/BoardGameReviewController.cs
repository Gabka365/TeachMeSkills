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
        private readonly BoardGameReviewRepositories _reviewRepositories;
        private readonly BoardGameMapper _mapper;
        private readonly HttpBoardGamesReviewsApiService _httpService;

        public BoardGameReviewController(BoardGameRepositories gameRepositories,
            BoardGameReviewRepositories reviewRepositories,
            BoardGameMapper mapper,
            HttpBoardGamesReviewsApiService httpService)
        {
            _gameRepositories = gameRepositories;
            _reviewRepositories = reviewRepositories;
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
        public IActionResult Create(BoardGameCreateReviewViewModel boardGameReviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameReviewViewModel);
            }

            DtoBoardGameReviewCreate review = _mapper.BuildBoardGameRewievDataModelFromCreate(boardGameReviewViewModel);
            review.BoardGameId = boardGameReviewViewModel.BoardGameId;
            _httpService.CreateReview(review);

            return RedirectToAction("BoardGame", "BoardGame", new { id = review.BoardGameId });
        }

        [HttpGet]
        public IActionResult Update(int id, int gameId)
        {
            DtoBoardGameReview reviewForUpdate = _httpService.GetReview(id);
            BoardGameUpdateReviewViewModel viewModel = _mapper.BuildBoardGameUpdateRewievViewModel(reviewForUpdate);
            viewModel.BoardGameId = gameId;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(BoardGameUpdateReviewViewModel boardGameReviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameReviewViewModel);
            }

            DtoBoardGameReviewUpdate updatedReview = _mapper.BuildBoardGameRewievDataModelFromUpdate(boardGameReviewViewModel);
            _httpService.UpdateReview(updatedReview);

            return RedirectToAction("BoardGame", "BoardGame", new { id = boardGameReviewViewModel.BoardGameId });
        }     
    }
}
