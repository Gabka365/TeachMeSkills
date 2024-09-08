using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Bson;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Hubs
{
    public class BlogCommentHub : Hub<IBlogCommentHub>
    { 
        private AuthService _authService;

        public BlogCommentHub(AuthService authService)
        {
            _authService = authService;
        }

        public void AddNewComment(string commentText, int postId)
        {
            var userName = _authService.IsAuthenticated()
                ? _authService.GetUserName()
                : "Guest";

            Clients.All.NotifyAboutNewComment(userName, commentText, DateTime.Now, postId);
        }
    }
}
