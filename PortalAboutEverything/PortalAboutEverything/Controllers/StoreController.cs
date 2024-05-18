using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Store;

namespace PortalAboutEverything.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        public IActionResult Good()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Good(CreateReviewViewModel viewModel)
        {
            var newReview = new CreateReviewViewModel
            {
                TitleOfReview = viewModel.TitleOfReview,
                DescriptionOfReview = viewModel.DescriptionOfReview
            };
            return View(newReview);
        }

    }
}
