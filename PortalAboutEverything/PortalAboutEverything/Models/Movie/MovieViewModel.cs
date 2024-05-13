namespace PortalAboutEverything.Models.Movie
{
    public class MovieViewModel
    {
        public DateOnly Date { get; set; }
        public List<int> AvailableRate { get; set; } = Enumerable.Range(1, 5).ToList();
    }
}
