using Microsoft.AspNetCore.SignalR;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Hubs
{
    public class GoodReviewHub : Hub<IGoodReviewHub>
    {
        private AuthService _authService;

        public GoodReviewHub(AuthService authService)
        {
            _authService = authService;
        }
        public void AddNewReview(int goodId, string reviewText)
        {
            var userName = _authService.IsAuthenticated()
               ? _authService.GetUserName()
               : "Гость";          
            // Отправка нового комментария всем подключенным клиентам
            Clients.All.NotifyAboutNewReview(goodId, userName, reviewText);
        }
    }
}
