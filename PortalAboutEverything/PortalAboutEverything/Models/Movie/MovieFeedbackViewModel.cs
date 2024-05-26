namespace PortalAboutEverything.Models.Movie
{
	public class MovieFeedbackViewModel
	{
		public List<int> AvailableRate { get; set; } = Enumerable.Range(1, 5).ToList();
	}
}
