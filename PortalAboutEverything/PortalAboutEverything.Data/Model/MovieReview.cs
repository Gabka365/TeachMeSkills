
namespace PortalAboutEverything.Data.Model
{
	public class MovieReview : BaseModel
	{
		public int Rate { get; set; }
		public DateTime DateOfCreation { get; set; }
		public string Comment { get; set; }

		public virtual Movie Movie { get; set; }
	}
}
