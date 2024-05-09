namespace PortalAboutEverything.Models.GameStore
{
    public class GameStoreIndexViewModel
    {
        public List<int> AvailableRates { get; set; } = Enumerable.Range(1, 5).ToList();

    }
}