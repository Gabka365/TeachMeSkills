using Microsoft.AspNetCore.Mvc;

namespace BookClubWebsite.Controllers
{
	public class BookController : Controller
	{
		public IActionResult BookPage()
		{
			return View();
		}
	}
}
