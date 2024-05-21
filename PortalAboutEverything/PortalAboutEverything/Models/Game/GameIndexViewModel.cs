namespace PortalAboutEverything.Models.Game
{
    public class GameIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int YearOfRelease { get; set; }

        public List<GameReviewViewModel> Reviews { get; set; }
    }
}
