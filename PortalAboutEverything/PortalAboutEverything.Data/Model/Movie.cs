using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Data.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReleaseDate { get; set; }
        public string Director { get; set; }
        public int Budget { get; set; }
        public string CountryOfOrigin { get; set; }
    }
}
