namespace PortalAboutEverything.Models.Game
{
    public class GameCreateViewModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int YearOfRelease { get; set; }
    }
}
