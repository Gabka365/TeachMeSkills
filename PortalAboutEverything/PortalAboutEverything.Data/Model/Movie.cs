using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Data.Model
{
    public class Movie : BaseModel
	{
        public string Name { get; set; }
        public string? Description { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public int Budget { get; set; }
        public string CountryOfOrigin { get; set; }

        public virtual List<User> UsersWhoFavoriteTheMovie { get; set; }
    }
}
