using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Model.Store;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.BookClub;
using PortalAboutEverything.Models.Game;
using PortalAboutEverything.Models.Store;
using PortalAboutEverything.Services;

namespace PortalAboutEverything.Controllers
{
    public class StoreController : Controller
    {
        private StoreRepositories _storeRepositories;

        private GoodReviewRepositories _goodReviewRepositories;

        private AuthService _authService;

        public StoreController(StoreRepositories storeRepositories, GoodReviewRepositories goodReviewRepositories, AuthService authService)
        {
            _storeRepositories = storeRepositories;
            _goodReviewRepositories = goodReviewRepositories;
            _authService = authService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var goodsViewModel = _storeRepositories.GetAll().Select(BuildStoreIndexViewModel).ToList();
            var viewModel = new BaseForStoreIndexViewModel
            {
                Goods = goodsViewModel,
                IsStoreAdmin = _authService.HasRoleOrHigher(UserRole.StoreAdmin),
            };

            return View(viewModel);
        }


        public IActionResult Good(int id)
        {
            var goodWithReview = _storeRepositories.GetGoodByIdWithReview(id);

            var goodViewModel = new GoodViewModel
            {
                Id = goodWithReview.Id,
                Name = goodWithReview.Name,
                Description = goodWithReview.Description,
                Price = goodWithReview.Price,
                Reviews = goodWithReview.Reviews?.Select(BuildGoodReviewViewModel).ToList(),
            };

            return View(goodViewModel);
        }

        public IActionResult FavouriteGoods()
        {
            var userName = _authService.GetUserName();

            var userId = _authService.GetUserId();
            var favouriteGoods = _storeRepositories.GetFavouriteGoodsBuUserId(userId);

            var viewModel = new FavouriteGoodsViewModel
            {
                UserName = userName,
                FavouriteGoods = favouriteGoods.Select(BuildStoreIndexViewModel).ToList(),
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddReview(AddGoodReviewViewModel viewModel)
        {

            _goodReviewRepositories.AddReview(viewModel.GoodId, viewModel.Text);

            return RedirectToAction("Good", new { id = viewModel.GoodId });
        }

        [HasRoleOrHigher(UserRole.StoreAdmin)]
        [HttpGet]
        public IActionResult AddGood()
        {
            return View();
        }

        [HasRoleOrHigher(UserRole.StoreAdmin)]
        [HttpPost]
        public IActionResult AddGood(GoodViewModel createGoodViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createGoodViewModel);
            }

            var good = new Good
            {
                Name = createGoodViewModel.Name!,
                Description = createGoodViewModel.Description!,
                Price = createGoodViewModel.Price!,
            };
            _storeRepositories.Create(good);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteGood(int id)
        {
            var model = _storeRepositories.GetGoodByIdWithReview(id);
            _storeRepositories.Delete(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateGood(int id)
        {
            var good = _storeRepositories.GetGoodForUpdate(id);
            var viewModel = BuildGoodUpdateViewModel(good);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateGood(GoodUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var good = new Good
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
            };
            _storeRepositories.UpdateGood(good);

            return RedirectToAction("Index");
        }

        private StoreIndexViewModel BuildStoreIndexViewModel(Good good)
        {
            return new StoreIndexViewModel
            {
                Id = good.Id,
                Name = good.Name,
                Description = good.Description,
                Price = good.Price,
            };
        }

        private GoodUpdateViewModel BuildGoodUpdateViewModel(Good good)
        {
            return new GoodUpdateViewModel
            {
                Id = good.Id,
                Name = good.Name,
                Description = good.Description,
                Price = good.Price,
            };
        }

        private AddGoodReviewViewModel BuildGoodReviewViewModel(GoodReview goodReview)
        {
            return new AddGoodReviewViewModel
            {
                Text = goodReview.Description,
            };
        }
    }
}
