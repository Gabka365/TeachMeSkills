
namespace PortalAboutEverything.Data.Model
{
    public class MovieReview
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public int Rate { get; set; }
        public DateTime DateOfCreation { get; set; }
		public string Comment { get; set; }

        public virtual Movie Movie { get; set; }
	}
}
