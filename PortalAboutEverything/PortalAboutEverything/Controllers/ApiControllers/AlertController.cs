using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model.Alerts;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Data.Repositories.Interfaces;
using PortalAboutEverything.Mappers;
using PortalAboutEverything.Models.Alert;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class AlertController : Controller
    {
        private AuthService _authService;
        private IAlertRepository _alertRepository;
        private AlertMapper _alertMapper;

        public AlertController(AuthService authService, IAlertRepository alertRepository, AlertMapper alertMapper)
        {
            _authService = authService;
            _alertRepository = alertRepository;
            _alertMapper = alertMapper;
        }

        public void UserWasNottified(int alertId)
        {
            if (!_authService.IsAuthenticated())
            {
                return;
            }

            var userId = _authService.GetUserId();
            _alertRepository.UserWasNottified(userId, alertId);
        }

        public List<AlertShortInfoViewModel> GetAlertWhatIMiss()
        {
            if (!_authService.IsAuthenticated())
            {
                return new List<AlertShortInfoViewModel>();
            }

            var userId = _authService.GetUserId();
            var alerts = _alertRepository
                .GetAlertsForUser(userId)
                .Select(_alertMapper.MapToShortInfo)
                .ToList();

            return alerts;
        }
    }
}
