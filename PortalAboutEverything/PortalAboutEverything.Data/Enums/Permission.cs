namespace PortalAboutEverything.Data.Enums
{
    [Flags]
    public enum Permission
    {
        None = 0,
        CanCreateGame = 1,      // 0001
        CanDeleteGame = 2,      // 0010
        CanViewPremission = 4,  // 0100
        CanEditPremission = 8,  // 1000
        CanCreateMovie = 16,
        CanDeleteMovie = 32,
        CanUpdateMovie = 64,
        CanLeaveReviewForMovie = 128,
        CanCreateAndUpdateBoardGames = 256,
        CanDeleteBoardGames = 512,
        CanModerateReviewsOfBoardGames = 1024,
        CanPostInBlog = 2048,
    }
}