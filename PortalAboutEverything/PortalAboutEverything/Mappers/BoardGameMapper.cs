using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Models.BoardGame;
using PortalAboutEverything.Services.AuthStuff;
using PortalAboutEverything.Services;
using PortalAboutEverything.Models.BoardGameReview;
using PortalAboutEverything.Data.Repositories.DataModel;
using BoardGamesReviewsApi.Dtos;
using PortalAboutEverything.Data.Repositories.Interfaces;

namespace PortalAboutEverything.Mappers
{
    public class BoardGameMapper
    {
        private readonly IAuthService _authServise;
        private readonly IPathHelper _pathHelper;
        private readonly IBoardGameRepositories _gameRepositories;

        public BoardGameMapper(IAuthService authService, IPathHelper pathHelper, IBoardGameRepositories gameRepositories)
        {
            _authServise = authService;
            _pathHelper = pathHelper;
            _gameRepositories = gameRepositories;
        }

        #region BoardGameBuilders
        public BoardGameViewModel BuildBoardGameViewModel(BoardGame game)
        => new BoardGameViewModel
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
            ProductCode = game.ProductCode
        };


        public BoardGame BuildBoardGameDataModelFromCreate(BoardGameCreateViewModel gameViewModel)
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

        public BoardGameUpdateViewModel BuildBoardGameUpdateDataModel(BoardGame game)
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

        public BoardGame BuildBoardGameDataModelFromUpdate(BoardGameUpdateViewModel gameViewModel)
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

        public BoardGameIndexViewModel BuildBoardGameIndexViewModel(BoardGame game)
            => new BoardGameIndexViewModel
            {
                Id = game.Id,
                Title = game.Title,
            };

        public FavoriteBoardGameIndexViewModel BuildFavoriteBoardGameIndexViewModel(Top3BoardGameDataModel game)
            => new FavoriteBoardGameIndexViewModel
            {
                Id = game.Id,
                Title = game.Title,
                CountOfUserWhoLikeIt = game.CountOfUserWhoLikeIt,
            };
        #endregion

        #region ReviewBuilders
        public DtoBoardGameReviewCreate BuildBoardGameRewievDataModelFromCreate(BoardGameCreateReviewViewModel reviewViewModel)
        => new DtoBoardGameReviewCreate
        {
            UserName = _authServise.GetUserName(),
            UserId = _authServise.GetUserId(),
            DateOfCreation = DateTime.Now,
            Text = reviewViewModel.Text,
        };

        public DtoBoardGameReviewUpdate BuildBoardGameRewievDataModelFromUpdate(BoardGameUpdateReviewViewModel reviewViewModel)
            => new DtoBoardGameReviewUpdate
            {
                Id = reviewViewModel.Id,
                Text = reviewViewModel.Text,
            };

        public BoardGameUpdateReviewViewModel BuildBoardGameUpdateRewievViewModel(DtoBoardGameReview review)
            => new BoardGameUpdateReviewViewModel
            {
                BoardGameName = _gameRepositories.GetName(review.BoardGameId),
                Text = review.Text,
            };
        #endregion
    }
}
