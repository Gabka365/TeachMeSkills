using Microsoft.AspNetCore.SignalR;
namespace PortalAboutEverything.Hubs
{
    public class BoardGameHub : Hub<IBoardGameHub>
    {
        public void DeleteBoardGame(int gameId)
        {
            Clients.All.NotifyAboutDeleteBoardGame(gameId);
        }

        public void ChangeFavorites()
        {
            Clients.All.NotifyAboutChangeFavorites();
        }
    }
}
