namespace PortalAboutEverything.Hubs
{
    public interface IBoardGameHub
    {
        Task NotifyAboutDeleteBoardGame(int gameId);
        Task NotifyAboutChangeFavorites();
    }
}
