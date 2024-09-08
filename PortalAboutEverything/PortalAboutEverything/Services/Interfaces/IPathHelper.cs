namespace PortalAboutEverything.Services.Interfaces
{
    public interface IPathHelper
    {
        string GetPathToPostCover(int postId);
        string GetPathToBoardGameMainImage(int boardGameId);
        string GetPathToBoardGameSideImage(int boardGameId);
        string GetPathToGameCover(int gameId);
        string GetPathToGameStoreCover(int gameId);
        string GetPathToGoodCover(int goodId);
        string GetPathToMovieImage(int movieId);
        string GetPathToTravelingImageFolder();
        bool IsPostCoverExist(int id);
        bool IsBoardGameMainImageExist(int id);
        bool IsBoardGameSideImageExist(int id);
        bool IsGameCoverExist(int id);
        bool IsGameStoreCoverExist(int id);
        bool IsGoodCoverExist(int id);
        bool IsMovieImageExist(int id);
    }
}