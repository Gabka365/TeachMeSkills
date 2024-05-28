namespace PortalAboutEverything.Models.Movie
{
	public class MovieAddReviewViewModel
	{
		public List<int> AvailableRate { get; set; } = Enumerable.Range(1, 5).ToList();
		public int MovieId { get; set; }
		public string Name { get; set; }
		public int Rate { get; set; }
		public DateTime DateOfCreation { get; set; }
		public string Comment { get; set; }
	}
}
