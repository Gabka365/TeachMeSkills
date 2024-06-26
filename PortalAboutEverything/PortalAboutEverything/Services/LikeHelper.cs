using PortalAboutEverything.Data.Model.Store;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Services
{
    public class LikeHelper
    {
        private AuthService _authService;

        private StoreRepositories _storeRepositories;
        public LikeHelper(AuthService authService, StoreRepositories storeRepositories)
        {
            _authService = authService;
            _storeRepositories = storeRepositories;
        }

        public bool IsGoodLikeExist(int id)
        {
            var user = _authService.GetUser();
            var goodById = _storeRepositories.GetGoodByIdWithLike(id);
            return goodById.UsersWhoLikedTheGood.Contains(user);
        }
    }
}
