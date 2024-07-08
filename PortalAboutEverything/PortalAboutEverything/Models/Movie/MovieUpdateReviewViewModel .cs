namespace PortalAboutEverything.Models.Movie
{
	public class MovieUpdateReviewViewModel
	{
		public int ReviewId { get; set; }
		public int MovieId { get; set; }
		public string MovieName { get; set; }
		public List<int> AvailableRate { get; set; } = Enumerable.Range(1, 5).ToList();
	}
}
