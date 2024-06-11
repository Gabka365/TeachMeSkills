using PortalAboutEverything.Data.Repositories.DataModel;

namespace PortalAboutEverything.Models.Movie
{
	public class IndexMovieAdminViewModel
	{
		public List<MovieIndexViewModel> Movies { get; set; }
		public bool IsMovieAdmin { get; set; }
		public List<MovieStatisticDataModel> MovieStatistics { get; set; }
	}
}
