using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Models.BoardGame;
using PortalAboutEverything.Services.AuthStuff;
using PortalAboutEverything.Services;
using PortalAboutEverything.Models.BoardGameReview;

namespace PortalAboutEverything.Mappers
{
    public class BoardGameMapper
    {
        private readonly AuthService _authServise;
        private readonly PathHelper _pathHelper;

        public BoardGameMapper(AuthService authService, PathHelper pathHelper)
        {
            _authServise = authService;
            _pathHelper = pathHelper;
        }

        #region BoardGameBuilders
        public BoardGameViewModel BuildBoardGameViewModel(BoardGame game)
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
        #endregion

        #region ReviewBuilders
        public BoardGameReviewViewModel BuildBoardGameRewievViewModel(BoardGameReview review)
            => new BoardGameReviewViewModel
            {
                Id = review.Id,
                Name = review.Name,
                DateOfCreationInStringFormat = review.DateOfCreation.ToString("dd.MM.yyyy HH:mm"),
                Text = review.Text,
            };


        public BoardGameReview BuildBoardGameRewievDataModelFromCreate(BoardGameCreateReviewViewModel reviewViewModel)
        => new BoardGameReview
        {
            Name = _authServise.GetUserName(),
            DateOfCreation = DateTime.Now,
            Text = reviewViewModel.Text,
        };

        public BoardGameReview BuildBoardGameRewievDataModelFromUpdate(BoardGameUpdateReviewViewModel reviewViewModel)
            => new BoardGameReview
            {
                Id = reviewViewModel.Id,
                Text = reviewViewModel.Text,
            };

        public BoardGameUpdateReviewViewModel BuildBoardGameUpdateRewievViewModel(BoardGameReview review)
            => new BoardGameUpdateReviewViewModel
            {
                BoardGameName = review.BoardGame.Title,
                Text = review.Text,
            };
        #endregion
    }
}
