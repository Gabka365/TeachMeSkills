using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.GameStore;

namespace PortalAboutEverything.Controllers
{
    public class GameStoreController : Controller
    {
        public IActionResult Index()
        {

            var viewModel = new GameStoreIndexViewModel
            {

            };


            return View(viewModel);
        }

        [HttpPost]
        public IActionResult RateTheGameStore(RateTheGameStoreViewModel rateTheGameStoreViewModel)
        {

            return View(rateTheGameStoreViewModel);
        }
    }

}