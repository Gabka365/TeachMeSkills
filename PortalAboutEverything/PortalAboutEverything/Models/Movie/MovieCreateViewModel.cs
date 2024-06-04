using Microsoft.Extensions.Localization;
using PortalAboutEverything.LocalizationResources;
using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.Movie
{
    public class MovieCreateViewModel
    {
        public string Name { get; set; }

        [MovieDescription]
        public string? Description { get; set; }

        [ReleaseYear]
        [Display(ResourceType = typeof(Movie_CreateMovie), Name = "ReleaseYear_Display")]
        public int ReleaseYear { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Movie_CreateMovie), 
            ErrorMessageResourceName = nameof(Movie_CreateMovie.RequiredNameDirector_Error))]
        [ForbiddenSymbols("#@%*<>")]
		[Display(ResourceType = typeof(Movie_CreateMovie), Name = "Director_Display")]
        public string Director { get; set; }

        public int Budget { get; set; }

		[Required(
			ErrorMessageResourceType = typeof(Movie_CreateMovie),
			ErrorMessageResourceName = nameof(Movie_CreateMovie.RequiredCountryOfOrigin_Error))]
		[ForbiddenSymbols("!#@%*?$№<>")]
		[Display(ResourceType = typeof(Movie_CreateMovie), Name = "CountryOfOrigin_Display")]
        public string CountryOfOrigin { get; set; }
    }
}
