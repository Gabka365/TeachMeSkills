using PortalAboutEverything.Data.Model.Alerts;
using PortalAboutEverything.Models.Alert;

namespace PortalAboutEverything.Mappers
{
    public class AlertMapper
    {
        public AlertCreateViewModel Map(Alert alert)
            => new AlertCreateViewModel
            {
                Id = alert.Id,
                Text = alert.Text,
                EndDate = alert.EndDate,
                IsActive = alert.IsActive,
            };

        public Alert MapToDataBaseModel(AlertCreateViewModel viewModel)
            => new Alert
            {
                Id = viewModel.Id,
                Text = viewModel.Text,
                EndDate = viewModel.EndDate,
                IsActive = viewModel.IsActive,
            };

        public AlertShortInfoViewModel MapToShortInfo(Alert alert)
            => new AlertShortInfoViewModel
            {
                Id = alert.Id,
                Text = alert.Text,
            };
    }
}
