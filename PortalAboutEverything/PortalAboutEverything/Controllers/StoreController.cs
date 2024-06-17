﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Model.Store;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Store;
using PortalAboutEverything.Services;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Controllers
{
    public class StoreController : Controller
    {
        private StoreRepositories _storeRepositories;

        private GoodReviewRepositories _goodReviewRepositories;

        private AuthService _authService;

        private PathHelper _pathHelper;

        public StoreController(StoreRepositories storeRepositories, GoodReviewRepositories goodReviewRepositories, AuthService authService, PathHelper pathHelper )
        {
            _storeRepositories = storeRepositories;
            _goodReviewRepositories = goodReviewRepositories;
            _authService = authService;
            _pathHelper = pathHelper;
        }

        [Authorize]
        public IActionResult Index()
        {
            var goodsViewModel = _storeRepositories.GetAll().Select(BuildGoodViewModel).ToList();
            var viewModel = new BaseForStoreIndexViewModel
            {
                Goods = goodsViewModel,
                IsAdmin = _authService.HasRoleOrHigher(UserRole.Admin),
                IsStoreAdmin = _authService.HasRoleOrHigher(UserRole.StoreAdmin)
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
                HasCover = _pathHelper.IsGoodCoverExist(id)
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

        [HasRoleOrHigher(UserRole.Admin)]
        [HttpGet]
        public IActionResult AddGood()
        {
            return View();
        }

        [HasRoleOrHigher(UserRole.Admin)]
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
                Price = createGoodViewModel.Price.Value,
            };

            _storeRepositories.Create(good);

            if (createGoodViewModel.Cover != null && createGoodViewModel.Cover.Length > 0)
            {
                var path = _pathHelper.GetPathToGoodCover(good.Id);

                using (var fs = new FileStream(path, FileMode.Create))
                {
                    createGoodViewModel.Cover.CopyTo(fs);
                }
            }


            return RedirectToAction("Index");
        }

        [HasRoleOrHigher(UserRole.Admin)]
        public IActionResult DeleteGood(int id)
        {
            var model = _storeRepositories.GetGoodByIdWithReview(id);
            _storeRepositories.Delete(model);

            var path = _pathHelper.GetPathToGoodCover(id);
            System.IO.File.Delete(path);
            return RedirectToAction("Index");
        }

        [HasRoleOrHigher(UserRole.StoreAdmin)]
        [HttpGet]
        public IActionResult UpdateGood(int id)
        {
            var good = _storeRepositories.GetGoodForUpdate(id);
            var viewModel = BuildGoodUpdateViewModel(good);
            return View(viewModel);
        }

        [HasRoleOrHigher(UserRole.StoreAdmin)]
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
                Price = viewModel.Price.Value,
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

        private GoodViewModel BuildGoodViewModel(Good good)
        {
            return new GoodViewModel
            {
                Id = good.Id,
                Name = good.Name,
                Description = good.Description,
                Price = good.Price,
                HasCover = _pathHelper.IsGoodCoverExist(good.Id),
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
