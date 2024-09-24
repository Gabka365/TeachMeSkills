using PortalAboutEverything.Data.Repositories.DataModel;

namespace PortalAboutEverything.Models.GameStore
{
    public class IndexGameViewModel
    {
        public List<GameStoreIndexViewModel> Games { get; set; }
        public bool CanCreateGameInGameStore { get; set; }
        public bool CanDeleteGameInGameStore { get; set; }
        public bool CanUpdateGameInGameStore { get; set; }
        public List<Top3BuyGamesDataModel> Top3BuyGamesDataModels { get; set; }
    }
}
