using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.BoardGame;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Services;
using Microsoft.AspNetCore.Authorization;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;

namespace PortalAboutEverything.Controllers
{
    [Authorize]
    public class BoardGameController : Controller
    {
        private BoardGameRepositories _gameRepositories;
        private BoardGameReviewRepositories _reviewRepositories;
        private UserRepository _userRepository;
        private AuthService _authServise;

        public BoardGameController(BoardGameRepositories gameRepositories,
            BoardGameReviewRepositories reviewRepositories,
            UserRepository userRepository,
            AuthService authService)
        {
            _gameRepositories = gameRepositories;
            _reviewRepositories = reviewRepositories;
            _userRepository = userRepository;
            _authServise = authService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var gamesViewModel = _gameRepositories
                .GetAll()
                .Select(BuildBoardGameIndexViewModel)
                .ToList();

            bool isBoardGameAdmin = false;
            if (_authServise.IsAuthenticated())
            {
                isBoardGameAdmin = _authServise.HasRoleOrHigher(UserRole.BoardGameAdmin);
            }

            var indexViewModel = new IndexViewModel()
            {
                BoardGames = gamesViewModel,
                IsBoardGameAdmin = isBoardGameAdmin
            };

            return View(indexViewModel);
        }

        [HttpGet]
        [HasPermission(Permission.CanCreateAndUpdateBoardGames)]
        public IActionResult CreateBoardGame()
        {
            return View();
        }

        [HttpPost]
        [HasPermission(Permission.CanCreateAndUpdateBoardGames)]
        public IActionResult CreateBoardGame(BoardGameCreateViewModel boardGameViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameViewModel);
            }

            BoardGame game = BuildBoardGameDataModelFromCreate(boardGameViewModel);

            _gameRepositories.Create(game);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [HasPermission(Permission.CanCreateAndUpdateBoardGames)]
        public IActionResult UpdateBoardGame(int id)
        {
            BoardGame boardGameForUpdate = _gameRepositories.Get(id);
            BoardGameUpdateViewModel viewModel = BuildBoardGameUpdateDataModel(boardGameForUpdate);

            return View(viewModel);
        }

        [HttpPost]
        [HasPermission(Permission.CanCreateAndUpdateBoardGames)]
        public IActionResult UpdateBoardGame(BoardGameUpdateViewModel boardGameViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameViewModel);
            }

            BoardGame updatedReview = BuildBoardGameDataModelFromUpdate(boardGameViewModel);
            _gameRepositories.Update(updatedReview);

            return RedirectToAction("Index");
        }

        [HasPermission(Permission.CanDeleteBoardGames)]
        public IActionResult DeleteBoardGame(int id)
        {
            _gameRepositories.Delete(id);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult BoardGame(int id)
        {
            BoardGame gameViewModel = _gameRepositories.GetWithReviews(id);
            BoardGameViewModel viewModel = BuildBoardGameViewModel(gameViewModel);

            if (_authServise.IsAuthenticated())
            {
                int userId = _authServise.GetUserId();
                User user = _userRepository.GetWithFavoriteBoardGames(userId);
                if (user.FavoriteBoardsGames.Any(boardGame => boardGame.Id == id))
                {
                    viewModel.IsFavoriteForUser = true;
                }
            }

            return View(viewModel);
        }

        public IActionResult UserFavoriteBoardGames()
        {
            string userName = _authServise.GetUserName();
            int userId = _authServise.GetUserId();

            List<BoardGame> favoriteBoardGames = _gameRepositories.GetFavoriteBoardGamesForUser(userId);

            UserFavoriteBoardGamesViewModel viewModel = new()
            {
                Name = userName,
                FavoriteBoardGames = favoriteBoardGames.Select(BuildBoardGameViewModel).ToList()
            };

            return View(viewModel);
        }

        public IActionResult AddFavoriteBoardGameForUser(int gameId)
        {
            User user = _authServise.GetUser();
            _gameRepositories.AddUserWhoFavoriteThisBoardGame(user, gameId);

            return RedirectToAction("BoardGame", new { id = gameId });
        }

        public IActionResult RemoveFavoriteBoardGameForUser(int gameId, bool isGamePage)
        {
            User user = _authServise.GetUser();
            _gameRepositories.RemoveUserWhoFavoriteThisBoardGame(user, gameId);

            if (isGamePage)
            {
                return RedirectToAction("BoardGame", new { id = gameId });
            }
            else
            {
                return RedirectToAction("UserFavoriteBoardGames");
            }
        }

        [HttpGet]
        public IActionResult CreateReview(int gameId)
        {
            BoardGameCreateReviewViewModel createReviewViewModel =
                new BoardGameCreateReviewViewModel
                {
                    BoardGameId = gameId,
                    BoardGameName = _gameRepositories.Get(gameId).Title,
                };

            return View(createReviewViewModel);
        }

        [HttpPost]
        public IActionResult CreateReview(BoardGameCreateReviewViewModel boardGameReviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameReviewViewModel);
            }

            BoardGameReview review = BuildBoardGameRewievDataModelFromCreate(boardGameReviewViewModel);
            _reviewRepositories.Create(review, boardGameReviewViewModel.BoardGameId);

            return RedirectToAction("BoardGame", new { id = review.BoardGame.Id });
        }

        [HttpGet]
        public IActionResult UpdateReview(int id, int gameId)
        {
            BoardGameReview reviewForUpdate = _reviewRepositories.GetWithBoardGame(id);
            BoardGameUpdateReviewViewModel viewModel = BuildBoardGameUpdateRewievViewModel(reviewForUpdate);
            viewModel.BoardGameId = gameId;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateReview(BoardGameUpdateReviewViewModel boardGameReviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameReviewViewModel);
            }

            BoardGameReview updatedReview = BuildBoardGameRewievDataModelFromUpdate(boardGameReviewViewModel);
            _reviewRepositories.Update(updatedReview, boardGameReviewViewModel.BoardGameId);

            return RedirectToAction("BoardGame", new { id = updatedReview.BoardGame.Id });
        }

        public IActionResult DeleteReview(int id, int gameId)
        {
            _reviewRepositories.Delete(id);

            return RedirectToAction("BoardGame", new { id = gameId });
        }

        #region BoardGameBuilders
        private BoardGameViewModel BuildBoardGameViewModel(BoardGame game)
        {
            List<BoardGameReviewViewModel> reviewViewModels = new();
            if (game.Reviews != null)
            {
                foreach (var review in game.Reviews)
                {
                    reviewViewModels.Add(BuildBoardGameRewievViewModel(review));
                }
            }

            return new BoardGameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                MiniTitle = game.MiniTitle,
                Description = game.Description,
                Essence = game.Essence,
                Tags = game.Tags,
                Price = game.Price,
                ProductCode = game.ProductCode,
                Reviews = reviewViewModels
            };
        }

        private BoardGame BuildBoardGameDataModelFromCreate(BoardGameCreateViewModel gameViewModel)
            => new BoardGame
            {
                Title = gameViewModel.Title,
                MiniTitle = gameViewModel.MiniTitle,
                Description = gameViewModel.Description,
                Essence = gameViewModel.Essence,
                Tags = gameViewModel.Tags,
                Price = gameViewModel.Price.Value,
                ProductCode = gameViewModel.ProductCode.Value,
            };

        private BoardGameUpdateViewModel BuildBoardGameUpdateDataModel(BoardGame game)
            => new BoardGameUpdateViewModel
            {
                OriginalTitle = game.Title,
                Title = game.Title,
                MiniTitle = game.MiniTitle,
                Description = game.Description,
                Essence = game.Essence,
                Tags = game.Tags,
                Price = game.Price,
                ProductCode = game.ProductCode,
            };

        private BoardGame BuildBoardGameDataModelFromUpdate(BoardGameUpdateViewModel gameViewModel)
             => new BoardGame
             {
                 Id = gameViewModel.Id,
                 Title = gameViewModel.Title,
                 MiniTitle = gameViewModel.MiniTitle,
                 Description = gameViewModel.Description,
                 Essence = gameViewModel.Essence,
                 Tags = gameViewModel.Tags,
                 Price = gameViewModel.Price.Value,
                 ProductCode = gameViewModel.ProductCode.Value,
             };

        private BoardGameIndexViewModel BuildBoardGameIndexViewModel(BoardGame game)
            => new BoardGameIndexViewModel
            {
                Id = game.Id,
                Title = game.Title,
            };
        #endregion

        #region ReviewBuilders
        private BoardGameReviewViewModel BuildBoardGameRewievViewModel(BoardGameReview review)
            => new BoardGameReviewViewModel
            {
                Id = review.Id,
                Name = review.Name,
                DateOfCreationInStringFormat = review.DateOfCreation.ToString("dd.MM.yyyy HH:mm"),
                Text = review.Text,
            };


        private BoardGameReview BuildBoardGameRewievDataModelFromCreate(BoardGameCreateReviewViewModel reviewViewModel)
            => new BoardGameReview
            {
                Name = _authServise.GetUserName(),
                DateOfCreation = DateTime.Now,
                Text = reviewViewModel.Text,
            };

        private BoardGameReview BuildBoardGameRewievDataModelFromUpdate(BoardGameUpdateReviewViewModel reviewViewModel)
            => new BoardGameReview
            {
                Id = reviewViewModel.Id,
                Text = reviewViewModel.Text,
            };

        private BoardGameUpdateReviewViewModel BuildBoardGameUpdateRewievViewModel(BoardGameReview review)
            => new BoardGameUpdateReviewViewModel
            {
                BoardGameName = review.BoardGame.Title,
                Text = review.Text,
            };
        #endregion
    }
}
