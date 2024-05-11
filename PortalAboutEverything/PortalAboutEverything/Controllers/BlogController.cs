using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.Blog;


namespace PortalAboutEverything.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendingMessage()
        {
            var now = DateTime.Now;
            var name = "Morgan Freeman";

            var viewModel = new BlogIndexViewModel
            {
                Now = now,
                Name = name,
            };

            return View(viewModel);
        }


        public IActionResult ReceivingMessage(ReceivingDataViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
