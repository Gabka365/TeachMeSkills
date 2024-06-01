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

        private GoodReviewRepositories _goodReviewRepositories;

        public StoreController(StoreRepositories storeRepositories, GoodReviewRepositories goodReviewRepositories)
        {
            _storeRepositories = storeRepositories;
            _goodReviewRepositories = goodReviewRepositories;
        }
        public IActionResult Index()
        {
            var goodsViewModel = _storeRepositories.GetAllGoods().Select(BuildStoreIndexViewModel).ToList();

            return View(goodsViewModel);
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

        [HttpPost]
        public IActionResult AddReview(AddGoodReviewViewModel viewModel)
        {
            
            _goodReviewRepositories.AddReview(viewModel.GoodId, viewModel.Text);

            return RedirectToAction("Good", new { id = viewModel.GoodId});
        }

        [HttpGet]
        public IActionResult AddGood()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGood(GoodViewModel createGoodViewModel)
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

        private GoodViewModel BuildGoodViewModel(Good good)
        {
            return new GoodViewModel
            {
                
                Name = good.Name,
                Description = good.Description,
                Price = good.Price,
                Reviews = good.Reviews?.Select(BuildGoodReviewViewModel).ToList(),
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
