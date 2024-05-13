using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Movie;

namespace PortalAboutEverything.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            var date = DateOnly.FromDateTime(DateTime.Now);
            var viewModel = new MovieViewModel
            {
                Date = date
            };

            return View(viewModel);
        }

        public IActionResult RateTheMovie(MovieRateViewModel rateViewModel)
        {
            return View(rateViewModel);
        }
    }
}
