using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Movie;
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

        public List<MovieIndexViewModel> GetAll()
        {
            var movieViewModel = _movieRepositories
                .GetAll()
                .Select(movie => new MovieIndexViewModel
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Description = movie.Description,
                    ReleaseYear = movie.ReleaseYear,
                    Director = movie.Director,
                    Budget = movie.Budget,
                    CountryOfOrigin = movie.CountryOfOrigin,
                    HasCover = _pathHelper.IsMovieImageExist(movie.Id),
                })
                .ToList();
            return movieViewModel;
        }

        public MovieIndexViewModel Get(int id)
        {
            var movie = _movieRepositories.Get(id);
            return new MovieIndexViewModel 
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                ReleaseYear = movie.ReleaseYear,
                Director = movie.Director,
                Budget = movie.Budget,
                CountryOfOrigin = movie.CountryOfOrigin,
                HasCover = _pathHelper.IsMovieImageExist(movie.Id),
            };
        }

        public void Remove(int id)
            => _movieRepositories.Delete(id);

        [HttpPost]
        public bool Create(MovieAddViewModel movie)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            var movieDb = new Movie
            {
                Name = movie.Name,
                Description = movie.Description,
                ReleaseYear = movie.ReleaseYear,
                Director = movie.Director,
                Budget = movie.Budget,
                CountryOfOrigin = movie.CountryOfOrigin,
            };

            _movieRepositories.Create(movieDb);

            return true;
        }

        [HttpPost]
        public bool Update(MovieAddViewModel movie)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            var movieDb = new Movie
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                ReleaseYear = movie.ReleaseYear,
                Director = movie.Director,
                Budget = movie.Budget,
                CountryOfOrigin = movie.CountryOfOrigin,
            };

            _movieRepositories.Update(movieDb);

            return true;
        }
    }
}
