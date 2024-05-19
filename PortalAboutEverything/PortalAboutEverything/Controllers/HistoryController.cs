using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.History;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Controllers
{
    public class HistoryController : Controller
    {
        private HistoryRepositories _historyRepositories;
        public HistoryController(HistoryRepositories historyRepository)
        {
            _historyRepositories = historyRepository;
        }
        public IActionResult Index()
        {       
            var eventsHistoryIndexViewModel = _historyRepositories
                .GetAll()
                .Select(BuildHistoryViewModel)
                .ToList();
            return View(eventsHistoryIndexViewModel);
        }

        

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEventViewModel createEventViewModel)
        {
            var historicalEvent = new History
            {
                Name = createEventViewModel.Name,
                Description = createEventViewModel.Description,
                Date = createEventViewModel.Date,
            };
            _historyRepositories.Create(historicalEvent);
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            _historyRepositories.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var historicalEvent = _historyRepositories.Get(id);
            var updateViewModel = BuildHistoryUpdateViewModel(historicalEvent);
            return View(updateViewModel);
        }

        [HttpPost]
        public IActionResult Update (HistoryUpdateViewModel updateViewModel)
        {
            var historicalEvents = new History
            {
                Id = updateViewModel.Id,
                Name = updateViewModel.Name,
                Description = updateViewModel.Description,
                Date = updateViewModel.Date,
            };
            _historyRepositories.Update(historicalEvents);

            return RedirectToAction("Index");
        }
        private HistoryIndexViewModel BuildHistoryViewModel(History historicalEvent)
            => new HistoryIndexViewModel
            {
                Id = historicalEvent.Id,
                Name = historicalEvent.Name,
                Date = historicalEvent.Date,
                Description = historicalEvent.Description,               
            };
        private HistoryUpdateViewModel BuildHistoryUpdateViewModel(History historicalEvent)
            => new HistoryUpdateViewModel
            {
                Id = historicalEvent.Id,
                Name = historicalEvent.Name,
                Date = historicalEvent.Date,
                Description = historicalEvent.Description,
            };
    }
}
