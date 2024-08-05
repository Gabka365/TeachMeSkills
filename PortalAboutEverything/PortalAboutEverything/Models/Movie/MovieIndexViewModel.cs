namespace PortalAboutEverything.Models.Movie
{
	public class MovieIndexViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int ReleaseYear { get; set; }
		public string Director { get; set; }
		public int Budget { get; set; }
		public string CountryOfOrigin { get; set; }
		public int AverageRate { get; set; }

		public bool HasCover { get; set; }
	}
}
