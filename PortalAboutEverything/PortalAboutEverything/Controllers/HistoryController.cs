using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.History;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Services;
using Microsoft.AspNetCore.Authorization;
using PortalAboutEverything.Controllers.ActionFilterAttributes;
using PortalAboutEverything.Data.Enums;

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
            var historyEventsViewModel = _historyRepositories
                .GetAll()
                .Select(BuildHistoryViewModel)
                .ToList();
            var viewmodel = new HistoryIndexViewModel
            {
                IsHistoryEventAdmin = _authService.HasRoleOrHigher(UserRole.HistoryEventAdmin),
                HistoryEvents = historyEventsViewModel,               
            };
            return View(viewmodel);
        }

        [HttpGet]
        [Authorize]
        [HasRoleOrHigher(UserRole.HistoryEventAdmin)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [HasRoleOrHigher(UserRole.HistoryEventAdmin)]
        public IActionResult Create(CreateHistoryEventViewModel createHistoryEventViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createHistoryEventViewModel);
            }

            var historicalEvent = new HistoryEvent
            {
                Name = createHistoryEventViewModel.Name,
                Description = createHistoryEventViewModel.Description,
                YearOfEvent = createHistoryEventViewModel.YearOfEvent,
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
        public IActionResult GuestProfile()
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

        private HistoryEventsViewModel BuildHistoryViewModel(HistoryEvent historicalEvent)
            => new HistoryEventsViewModel
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
