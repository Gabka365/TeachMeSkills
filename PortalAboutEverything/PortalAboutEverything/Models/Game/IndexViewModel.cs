namespace PortalAboutEverything.Models.Game
{
    public class IndexViewModel
    {
        public List<GameIndexViewModel> Games { get; set; }
        public bool CanCreateGame { get; set; }
        public bool CanDeleteGame { get; set; }
    }
}
