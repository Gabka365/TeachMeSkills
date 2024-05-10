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

        public IActionResult Good(CreateReviewViewModel viewModel)
        {
            var show = new ShowNewModel
            {
                Title = viewModel.Title,
                Description = viewModel.Description
            };

            return View(show);
        }

        [HttpGet]
        public IActionResult AddReview(int Id)
        {
            return View();
        }



        [HttpPost]
        public IActionResult AddReview()
        {
            return RedirectToAction("Good");
        }
    }
}
