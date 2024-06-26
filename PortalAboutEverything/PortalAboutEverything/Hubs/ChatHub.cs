using Microsoft.AspNetCore.SignalR;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        private AuthService _authService;

        public ChatHub(AuthService authService)
        {
            _authService = authService;
        }

        public void AddNewMessage(string messageText)
        {
            var userName = _authService.IsAuthenticated()
                ? _authService.GetUserName()
                : "Гость";
            Clients.All.NotifyAboutNewMessage(userName, messageText);
            //Clients.All.SendAsync("NotifyAboutNewMessage", userName, messageText);
        }

        public void UserConnectToChat()
        {
            var userName = _authService.IsAuthenticated()
                ? _authService.GetUserName()
                : "Гость";
            Clients.All.NotifyAboutNewUser(userName);
        }
    }
}
