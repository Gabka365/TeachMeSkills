using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.BoardGame;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Services.AuthStuff;
using PortalAboutEverything.Services;

namespace PortalAboutEverything.Controllers
{
    [Authorize]
    public class BoardGameController : Controller
    {
        private readonly BoardGameRepositories _gameRepositories;
        private readonly BoardGameReviewRepositories _reviewRepositories;
        private readonly UserRepository _userRepository;
        private readonly AuthService _authServise;
        private readonly PathHelper _pathHelper;

        public BoardGameController(BoardGameRepositories gameRepositories,
            BoardGameReviewRepositories reviewRepositories,
            UserRepository userRepository,
            AuthService authService,
            PathHelper pathHelper)
        {
            _gameRepositories = gameRepositories;
            _reviewRepositories = reviewRepositories;
            _userRepository = userRepository;
            _authServise = authService;
            _pathHelper = pathHelper;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var gamesViewModel = _gameRepositories
                .GetAll()
                .Select(BuildBoardGameIndexViewModel)
                .ToList();

            var canCreateAndUpdate = false;
            var canDelete = false;
            if (_authServise.IsAuthenticated())
            {
                canCreateAndUpdate = _authServise.HasPermission(Permission.CanCreateAndUpdateBoardGames);
                canDelete = _authServise.HasPermission(Permission.CanDeleteBoardGames);
            }

            var indexViewModel = new IndexViewModel()
            {
                BoardGames = gamesViewModel,
                CanCreateAndUpdateBoardGames = canCreateAndUpdate,
                CanDeleteBoardGames = canDelete
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

            var pathToMainImage = _pathHelper.GetPathToBoardGameMainImage(game.Id);
            using (var fs = new FileStream(pathToMainImage, FileMode.Create))
            {
                boardGameViewModel.MainImage.CopyTo(fs);
            }

            if (boardGameViewModel.SideImage is not null)
            {
                var pathToSideImage = _pathHelper.GetPathToBoardGameSideImage(game.Id);
                using (var fs = new FileStream(pathToSideImage, FileMode.Create))
                {
                    boardGameViewModel.SideImage.CopyTo(fs);
                }
            }

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

            var pathToMainImage = _pathHelper.GetPathToBoardGameMainImage(boardGameViewModel.Id);
            System.IO.File.Delete(pathToMainImage);
            using (var fs = new FileStream(pathToMainImage, FileMode.Create))
            {
                boardGameViewModel.MainImage.CopyTo(fs);
            }

            if (_pathHelper.IsBoardGameSideImageExist(boardGameViewModel.Id))
            {
                var pathToSideImage = _pathHelper.GetPathToBoardGameSideImage(boardGameViewModel.Id);
                System.IO.File.Delete(pathToSideImage);
            }

            if (boardGameViewModel.SideImage is not null)
            {
                var pathToSideImage = _pathHelper.GetPathToBoardGameSideImage(boardGameViewModel.Id);
                using (var fs = new FileStream(pathToSideImage, FileMode.Create))
                {
                    boardGameViewModel.SideImage.CopyTo(fs);
                }
            }

            return RedirectToAction("Index");
        }

        [HasPermission(Permission.CanDeleteBoardGames)]
        public IActionResult DeleteBoardGame(int id)
        {
            _gameRepositories.Delete(id);

            var pathToMainImage = _pathHelper.GetPathToBoardGameMainImage(id);
            System.IO.File.Delete(pathToMainImage);

            if (_pathHelper.IsBoardGameSideImageExist(id))
            {
                var pathToSideImage = _pathHelper.GetPathToBoardGameSideImage(id);
                System.IO.File.Delete(pathToSideImage);
            }

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
            if (game.Reviews is not null)
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
                HasMainImage = _pathHelper.IsBoardGameMainImageExist(game.Id),
                HasSideImage = _pathHelper.IsBoardGameSideImageExist(game.Id),
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
