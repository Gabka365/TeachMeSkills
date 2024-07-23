using Microsoft.Extensions.Localization;
using PortalAboutEverything.LocalizationResources;
using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.Movie
{
    public class MovieAddViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int ReleaseYear { get; set; }

        public string Director { get; set; }

        public int Budget { get; set; }

        public string CountryOfOrigin { get; set; }

    }
}
