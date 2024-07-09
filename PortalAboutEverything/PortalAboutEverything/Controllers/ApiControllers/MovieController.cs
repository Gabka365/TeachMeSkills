using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Services;

namespace PortalAboutEverything.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class MovieController : Controller
    {
        private MovieRepositories _movieRepositories;
        private PathHelper _pathHelper;

        public MovieController(MovieRepositories movieRepositories, PathHelper pathHelper)
        {
            _movieRepositories = movieRepositories;
            _pathHelper = pathHelper;
        }

        [Authorize]
        [HasPermission(Permission.CanDeleteMovie)]
        public bool DeleteMovie(int movieId)
        {
            _movieRepositories.Delete(movieId);
            var path = _pathHelper.GetPathToMovieImage(movieId);
            System.IO.File.Delete(path);

            return !_movieRepositories.Exist(movieId);
        }

        public List<int> FindAllMovieId()
        {
            return _movieRepositories.FindAllMovieId();
        }
    }
}
