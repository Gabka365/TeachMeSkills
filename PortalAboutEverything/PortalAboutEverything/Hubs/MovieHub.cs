using Microsoft.AspNetCore.SignalR;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Hubs
{
    public class MovieHub : Hub<IMovieHub>
    {
        private AuthService _authService;
        private MovieRepositories _movieRepositories;

        public MovieHub(AuthService authService, MovieRepositories movieRepositories)
        {
            _authService = authService;
            _movieRepositories = movieRepositories;
        }

        public void UserDeleteMovie(int movieId)
        {
            var userName = _authService.IsAuthenticated()
                ? _authService.GetUserName()
                : "Гость";
            var movieName = _movieRepositories.Get(movieId).Name;
            Clients.All.NotifyAboutDelitingMovie(userName, movieName);
        }
    }
}
