using PortalAboutEverything.LocalizationResources;
using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.Game.Api
{
    public class CreateGameViewModel
    {
        [Required(
            ErrorMessageResourceType = typeof(Game_Index),
            ErrorMessageResourceName = nameof(Game_Index.RequiredName_ErrorMessage))]
        public string Name { get; set; }

        public string? Desc { get; set; }

        [ReleaseYear(
            ErrorMessageResourceType = typeof(Game_Index),
            ErrorMessageResourceName = nameof(Game_Index.RelaseDate_ValidationErrorMessage))]
        [Display(Name = "Год выпуска игры")]
        public int YearOfRelease { get; set; }
    }
}
