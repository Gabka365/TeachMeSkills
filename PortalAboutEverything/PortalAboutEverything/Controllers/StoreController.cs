using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Model.Store;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.BookClub;
using PortalAboutEverything.Models.Game;
using PortalAboutEverything.Models.Store;

namespace PortalAboutEverything.Controllers
{
    public class StoreController : Controller
    {
        private StoreRepositories _storeRepositories;

        public StoreController(StoreRepositories storeRepositories)
        {
            _storeRepositories = storeRepositories;
        }
        public IActionResult Index()
        {
            var goodsViewModel = _storeRepositories.GetAllGoods().Select(BuildStoreIndexViewModel).ToList();

            return View(goodsViewModel);
        }

        public IActionResult Good()
        {
            var reviewsViewModel = _storeRepositories.GetAllReviews().Select(BuildGoodReviewViewModel).ToList();
            return View(reviewsViewModel);
        }

        [HttpGet]
        public IActionResult AddReview()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddReview(CreateReviewViewModel viewModel)
        {
            var newReview = new GoodReview
            {
                Title = viewModel.Title,
                Description = viewModel.Description
            };
            _storeRepositories.AddReview(newReview);

            return RedirectToAction("Good");
        }

        [HttpGet]
        public IActionResult AddGood()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGood(AddGoodViewModel createGoodViewModel)
        {
            var good = new Good
            {
                Name = createGoodViewModel.Name,
                Description = createGoodViewModel.Description,
                Price = createGoodViewModel.Price,

            };
            _storeRepositories.AddGood(good);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteGood(int id)
        {
            _storeRepositories.Delete(id);
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

        private CreateReviewViewModel BuildGoodReviewViewModel(GoodReview review)
        {
            return new CreateReviewViewModel
            {
                Id = review.Id,
                Title = review.Title,
                Description = review.Description,
            };
        }
    }
}
