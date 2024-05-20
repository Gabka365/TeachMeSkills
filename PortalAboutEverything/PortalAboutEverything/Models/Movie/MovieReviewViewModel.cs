namespace PortalAboutEverything.Models.Movie
{
	public class MovieReviewViewModel
	{
		public List<int> AvailableRate { get; set; } = Enumerable.Range(1, 5).ToList();
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
