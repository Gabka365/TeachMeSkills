using Microsoft.AspNetCore.SignalR;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Hubs
{
    public class BoardGameHub : Hub<IBoardGameHub>
    {
        public void DeleteBoardGame(int gameId)
        {
            Clients.All.NotifyAboutDeleteBoardGame(gameId);
        }
    }
}
