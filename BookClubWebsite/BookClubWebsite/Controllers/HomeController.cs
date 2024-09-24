//using BookClubWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookClubWebsite.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult BookPage()
        {
            return View();
        }

       public IActionResult Index()
        { 
            return View();
        }

  
    }
}
