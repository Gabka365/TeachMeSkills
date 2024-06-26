using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Mappers;
using PortalAboutEverything.Models.BoardGameReview;
using PortalAboutEverything.Services;
using PortalAboutEverything.Services.Dtos;

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

            DtoBoardGameReview review = _mapper.BuildBoardGameRewievDataModelFromCreate(boardGameReviewViewModel);
            review.BoardGameId = boardGameReviewViewModel.BoardGameId;
            _httpService.CreateReview(review);

            return RedirectToAction("BoardGame", "BoardGame", new { id = review.BoardGameId });
        }

        [HttpGet]
        public IActionResult Update(int id, int gameId)
        {
            BoardGameReview reviewForUpdate = _reviewRepositories.GetWithBoardGame(id);
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

            BoardGameReview updatedReview = _mapper.BuildBoardGameRewievDataModelFromUpdate(boardGameReviewViewModel);
            _reviewRepositories.Update(updatedReview, boardGameReviewViewModel.BoardGameId);

            //return RedirectToPage($"/BoardGame/BoardGame?Id={updatedReview.BoardGame!.Id}");
            return RedirectToAction("BoardGame", "BoardGame", new { id = updatedReview.BoardGame!.Id });
        }

        public IActionResult Delete(int id, int gameId)
        {
            _reviewRepositories.Delete(id);

            //return RedirectToPage($"/BoardGame/BoardGame?Id={gameId}");
            return RedirectToAction("BoardGame", "BoardGame", new { id = gameId });
        }

        
    }
}
