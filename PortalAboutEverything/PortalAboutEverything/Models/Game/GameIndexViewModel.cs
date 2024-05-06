namespace PortalAboutEverything.Models.Game
{
    public class GameIndexViewModel
    {
        public int Second { get; set; }
        public string DayName { get; set; }
        public List<string> Days { get; set; }
        public List<int> AvailableRates { get; set; } = Enumerable.Range(1, 10).ToList();
    }
}
