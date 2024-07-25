using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.BoardGame;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Services.AuthStuff;
using PortalAboutEverything.Services;
using PortalAboutEverything.Mappers;
using System.Reflection;
using PortalAboutEverything.Services.Apis;

namespace PortalAboutEverything.Controllers
{
    [Authorize]
    public class BoardGameController : Controller
    {
        private readonly BoardGameRepositories _gameRepositories;
        private readonly UserRepository _userRepository;
        private readonly AuthService _authServise;
        private readonly PathHelper _pathHelper;
        private readonly BoardGameMapper _mapper;
        private readonly HttpBoardGameOfDayServise _boardGameOfDayServise;
        private readonly HttpBestBoardGameServise _bestBoardGameServise;

        public BoardGameController(BoardGameRepositories gameRepositories,
            UserRepository userRepository,
            AuthService authService,
            PathHelper pathHelper,
            BoardGameMapper mapper,
            HttpBoardGameOfDayServise boardGameOfDayServise,
            HttpBestBoardGameServise bestBoardGameServise)
        {
            _gameRepositories = gameRepositories;
            _userRepository = userRepository;
            _authServise = authService;
            _pathHelper = pathHelper;
            _mapper = mapper;
            _boardGameOfDayServise = boardGameOfDayServise;
            _bestBoardGameServise = bestBoardGameServise;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var top3BoardGames = _gameRepositories
                .GetTop3()
                .Select(_mapper.BuildFavoriteBoardGameIndexViewModel)
                .ToList();

            var gamesViewModel = _gameRepositories
                .GetAll()
                .Select(_mapper.BuildBoardGameIndexViewModel)
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
                CanDeleteBoardGames = canDelete,
                Top3FavoriteBoardGames = top3BoardGames,
            };

            return View(indexViewModel);
        }

        [HttpGet]
        [HasPermission(Permission.CanCreateAndUpdateBoardGames)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [HasPermission(Permission.CanCreateAndUpdateBoardGames)]
        public IActionResult Create(BoardGameCreateViewModel boardGameViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameViewModel);
            }

            BoardGame game = _mapper.BuildBoardGameDataModelFromCreate(boardGameViewModel);

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

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [HasPermission(Permission.CanCreateAndUpdateBoardGames)]
        public IActionResult Update(int id)
        {
            BoardGame boardGameForUpdate = _gameRepositories.Get(id);
            BoardGameUpdateViewModel viewModel = _mapper.BuildBoardGameUpdateDataModel(boardGameForUpdate);

            return View(viewModel);
        }

        [HttpPost]
        [HasPermission(Permission.CanCreateAndUpdateBoardGames)]
        public IActionResult Update(BoardGameUpdateViewModel boardGameViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameViewModel);
            }

            BoardGame updatedReview = _mapper.BuildBoardGameDataModelFromUpdate(boardGameViewModel);
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

            return RedirectToAction(nameof(Index));
        }

        [HasPermission(Permission.CanDeleteBoardGames)]
        public IActionResult Delete(int id)
        {
            _gameRepositories.Delete(id);

            var pathToMainImage = _pathHelper.GetPathToBoardGameMainImage(id);
            System.IO.File.Delete(pathToMainImage);

            if (_pathHelper.IsBoardGameSideImageExist(id))
            {
                var pathToSideImage = _pathHelper.GetPathToBoardGameSideImage(id);
                System.IO.File.Delete(pathToSideImage);
            }

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> BoardGame(int id)
        {
            var boardGame = _gameRepositories.Get(id)!;
            var viewModel = _mapper.BuildBoardGameViewModel(boardGame);

            if (_authServise.IsAuthenticated())
            {
                int userId = _authServise.GetUserId();
                viewModel.CurrentUserId = userId;

                User user = _userRepository.GetWithFavoriteBoardGames(userId);
                if (user.FavoriteBoardsGames.Any(boardGame => boardGame.Id == id))
                {
                    viewModel.IsFavoriteForUser = true;
                }
                if (_authServise.HasPermission(Permission.CanModerateReviewsOfBoardGames))
                {
                    viewModel.IsModerator = true;
                }
            }
            else
            {
                viewModel.CurrentUserId = -1;
            }

            var boardGameOfDay = _boardGameOfDayServise.GetBoardGameOfDayAsync();
            var bestBoardGame = _bestBoardGameServise.GetBestBoardGameAsync();

            await Task.WhenAll(boardGameOfDay,  bestBoardGame);

            var boardGameOfDayViewModel = _mapper.BuildBoardGameOfDayViewModel(boardGameOfDay.Result);
            var bestBoardGameViewModel = _mapper.BuildBestBoardGameViewModel(bestBoardGame.Result);

            viewModel.BoardGameOfDay = boardGameOfDayViewModel;
            viewModel.BestBoardGame = bestBoardGameViewModel;

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
                FavoriteBoardGames = favoriteBoardGames.Select(_mapper.BuildBoardGameViewModel).ToList()
            };

            return View(viewModel);
        }

        public IActionResult RemoveFavoriteBoardGameForUser(int gameId, bool isGamePage)
        {
            User user = _authServise.GetUser();
            _gameRepositories.RemoveUserWhoFavoriteThisBoardGame(user, gameId);

            if (isGamePage)
            {
                return RedirectToAction(nameof(BoardGame), new { id = gameId });
            }
            else
            {
                return RedirectToAction(nameof(UserFavoriteBoardGames));
            }
        }

        public IActionResult ApiMethods()
        {
            List<ApiMethodViewModel> viewModel = new();

            var apiType = typeof(ApiControllers.BoardGameController);
            var apiMethods = apiType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var method in apiMethods)
            {
                List<ParameterViewModel> parametersViewModel = new();
                var parameters = method.GetParameters();
                foreach (var parameter in parameters)
                {
                    var parameterViewModel = new ParameterViewModel { ParameterTitle = parameter.Name, ParameterTypeName = GetTypeName(parameter.ParameterType) };
                    parametersViewModel.Add(parameterViewModel);
                }

                var methodViewModel = new ApiMethodViewModel { Name = method.Name, ReturnTypeName = GetTypeName(method.ReturnType), Parameters = parametersViewModel };

                viewModel.Add(methodViewModel);
            }

            return View(viewModel);
        }

        private string GetTypeName(Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }

            var genericType = type.GetGenericTypeDefinition();
            var genericArguments = type.GetGenericArguments();
            var genericTypeName = genericType.Name.Split('`')[0];
            var argumentTypeNames = string.Join(", ", genericArguments.Select(GetTypeName));
            return $"{genericTypeName}<{argumentTypeNames}>";
        }
    }
}
