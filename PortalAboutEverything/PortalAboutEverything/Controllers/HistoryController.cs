using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.History;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Services;

namespace PortalAboutEverything.Controllers
{
    public class HistoryController : Controller
    {
        private HistoryRepositories _historyRepositories;
        private AuthService _authService;
        public HistoryController(HistoryRepositories historyRepository, AuthService authService)
        {
            _historyRepositories = historyRepository;
            _authService = authService;
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
            var historicalEvent = new HistoryEvent
            {
                Name = createEventViewModel.Name,
                Description = createEventViewModel.Description,
                YearOfEvent = createEventViewModel.YearOfEvent,
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
            var historicalEvents = new HistoryEvent
            {
                Id = updateViewModel.Id,
                Name = updateViewModel.Name,
                Description = updateViewModel.Description,
                YearOfEvent = updateViewModel.YearOfEvent,
            };
            _historyRepositories.Update(historicalEvents);

            return RedirectToAction("Index");
        }
        public IActionResult Guest()
        {
            var userName = _authService.GetUserName();

            var userId = _authService.GetUserId();
            var historyEvents = _historyRepositories.GetFavoriteHistoryEventsByUserId(userId);

            var viewModel = new GuestViewModel
            {
                Name = userName,
                HistoryEvents = historyEvents
                    .Select(BuildFavoriteHistoryEventViewModel)
                    .ToList(),
            };

            return View(viewModel);
        }

        private HistoryIndexViewModel BuildHistoryViewModel(HistoryEvent historicalEvent)
            => new HistoryIndexViewModel
            {
                Id = historicalEvent.Id,
                Name = historicalEvent.Name,
                YearOfEvent = historicalEvent.YearOfEvent,
                Description = historicalEvent.Description,               
            };

        private HistoryUpdateViewModel BuildHistoryUpdateViewModel(HistoryEvent historicalEvent)
            => new HistoryUpdateViewModel
            {
                Id = historicalEvent.Id,
                Name = historicalEvent.Name,
                YearOfEvent = historicalEvent.YearOfEvent,
                Description = historicalEvent.Description,
            };

        private FavoriteHistoryEventsViewModel BuildFavoriteHistoryEventViewModel(HistoryEvent historicalEvent)
           => new FavoriteHistoryEventsViewModel
           {
               Id = historicalEvent.Id,
               Name = historicalEvent.Name,
               YearOfEvent = historicalEvent.YearOfEvent,
               Description = historicalEvent.Description,
           };
    }
}
