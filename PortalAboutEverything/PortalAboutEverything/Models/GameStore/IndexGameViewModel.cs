namespace PortalAboutEverything.Models.GameStore
{
    public class IndexGameViewModel
    {
        public List<GameStoreIndexViewModel> Games { get; set; }
        public bool CanCreateGameInGameStore { get; set; }
        public bool CanDeleteGameInGameStore { get; set; }
        public bool CanUpdateGameInGameStore { get; set; }
    }
}
