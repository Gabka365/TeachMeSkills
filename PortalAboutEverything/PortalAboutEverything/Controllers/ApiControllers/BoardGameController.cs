using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Services;
using PortalAboutEverything.Models.BoardGame;
using PortalAboutEverything.Mappers;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories.Interfaces;
using PortalAboutEverything.Services.Interfaces;
using PortalAboutEverything.Services.AuthStuff.Interfaces;
using PortalAboutEverything.Data.CacheServices;

namespace PortalAboutEverything.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    [Authorize]
    public class BoardGameController : Controller
    {
        private readonly IPathHelper _pathHelper;
        private readonly IBoardGameRepositories _gameRepositories;
        private readonly IUserRepository _userRepository;
        private readonly BoardGameMapper _mapper;
        private readonly IAuthService _authServise;
        private readonly LocalizatoinService _localizatoinService;
        private readonly BoardGameCache _cache;

        public BoardGameController(IPathHelper pathHelper,
            IBoardGameRepositories gameRepositories,
            IUserRepository userRepository,
            BoardGameMapper mapper,
            IAuthService authServise,
            LocalizatoinService localizatoinService,
            BoardGameCache cache)
        {
            _pathHelper = pathHelper;
            _gameRepositories = gameRepositories;
            _userRepository = userRepository;
            _mapper = mapper;
            _authServise = authServise;
            _localizatoinService = localizatoinService;
            _cache = cache;
        }

        [AllowAnonymous]
        public bool Create(BoardGameCreateViewModelReact boardGameViewModel)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            BoardGame game = _mapper.BuildBoardGameDataModelFromCreateReact(boardGameViewModel);

            _gameRepositories.Create(game);

            return true;
        } // For react

        [HasPermission(Permission.CanDeleteBoardGames)]
        [AllowAnonymous] // For react
        public bool Delete(int id)
        {
            if (!_gameRepositories.Delete(id))
            {
                return false;
            }

            if (_pathHelper.IsBoardGameMainImageExist(id))
            {
                var pathToMainImage = _pathHelper.GetPathToBoardGameMainImage(id);
                System.IO.File.Delete(pathToMainImage);
            }

            if (_pathHelper.IsBoardGameSideImageExist(id))
            {
                var pathToSideImage = _pathHelper.GetPathToBoardGameSideImage(id);
                System.IO.File.Delete(pathToSideImage);
            }

            _cache.ResetCache();

            return true;
        }

        public void AddFavoriteBoardGameForUser(int gameId)
        {
            User user = _authServise.GetUser();
            _gameRepositories.AddUserWhoFavoriteThisBoardGame(user, gameId);
        }

        public void RemoveFavoriteBoardGameForUser(int gameId)
        {
            User user = _authServise.GetUser();
            _gameRepositories.RemoveUserWhoFavoriteThisBoardGame(user, gameId);
        }

        [AllowAnonymous]
        public List<FavoriteBoardGameIndexViewModel> GetTop3()
        {
            return _gameRepositories
                .GetTop3()
                .Select(_mapper.BuildFavoriteBoardGameIndexViewModel)
                .ToList();
        }

        [AllowAnonymous]
        public List<BoardGameIndexViewModel> GetAll()
        {
            return _gameRepositories
                .GetAll()
                .Select(_mapper.BuildBoardGameIndexViewModel)
                .ToList();
        }

        [AllowAnonymous]
        public BoardGameViewModel Get(int id)
        {
            BoardGame gameViewModel = _gameRepositories.Get(id)!;
            BoardGameViewModel viewModel = _mapper.BuildBoardGameViewModel(gameViewModel);

            if (_authServise.IsAuthenticated())
            {
                int userId = _authServise.GetUserId();
                User user = _userRepository.GetWithFavoriteBoardGames(userId);
                if (user.FavoriteBoardsGames.Any(boardGame => boardGame.Id == id))
                {
                    viewModel.IsFavoriteForUser = true;
                }
            }

            return viewModel;

        }

        [AllowAnonymous]
        public string GetCorrectTextForAlert(string text)
        {
            return _localizatoinService.GetLocalizedNewBoardGameAlert(text);
        }
    }
}
