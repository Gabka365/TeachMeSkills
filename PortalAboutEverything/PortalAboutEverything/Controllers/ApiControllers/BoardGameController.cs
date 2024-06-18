using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Services;

namespace PortalAboutEverything.Controllers.ApiControllers
{
    [ApiController]
    [Route("/BoardGame/api/[controller]/[action]")]
    [Authorize]
    public class BoardGameController : Controller
    {
        private readonly PathHelper _pathHelper;
        private readonly BoardGameRepositories _gameRepositories;

        public BoardGameController(PathHelper pathHelper, BoardGameRepositories gameRepositories)
        {
            _pathHelper = pathHelper;
            _gameRepositories = gameRepositories;
        }

        [HasPermission(Permission.CanDeleteBoardGames)]
        public void Delete(int id)
        {
            _gameRepositories.Delete(id);

            var pathToMainImage = _pathHelper.GetPathToBoardGameMainImage(id);
            System.IO.File.Delete(pathToMainImage);

            if (_pathHelper.IsBoardGameSideImageExist(id))
            {
                var pathToSideImage = _pathHelper.GetPathToBoardGameSideImage(id);
                System.IO.File.Delete(pathToSideImage);
            }
        }
    }
}
