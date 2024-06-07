using PortalAboutEverything.LocalizationResources.BoardGame;
using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.BoardGame
{
    public class BoardGameUpdateViewModel
    {
        public int Id { get; set; }
        public string OriginalTitle { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(BoardGame_CreateAndUpdateGame),
            ErrorMessageResourceName = nameof(BoardGame_CreateAndUpdateGame.RequiredTitle_ErrorMessage))]
        [TextInput(2, 35)]
        [Display(
            ResourceType = typeof(BoardGame_CreateAndUpdateGame),
            Name = nameof(BoardGame_CreateAndUpdateGame.DisplayTitle_Name))]
        public string Title { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(BoardGame_CreateAndUpdateGame),
            ErrorMessageResourceName = nameof(BoardGame_CreateAndUpdateGame.RequiredMiniTitle_ErrorMessage))]
        [TextInput(2, 50)]
        [Display(
            ResourceType = typeof(BoardGame_CreateAndUpdateGame),
            Name = nameof(BoardGame_CreateAndUpdateGame.DisplayMiniTitle_Name))]
        public string MiniTitle { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(BoardGame_CreateAndUpdateGame),
            ErrorMessageResourceName = nameof(BoardGame_CreateAndUpdateGame.RequiredDescription_ErrorMessage))]
        [TextInput(90)]
        [Display(
            ResourceType = typeof(BoardGame_CreateAndUpdateGame),
            Name = nameof(BoardGame_CreateAndUpdateGame.DisplayDescription_Name))]
        public string Description { get; set; }
        public string? Essence { get; set; }
        public string? Tags { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(BoardGame_CreateAndUpdateGame),
            ErrorMessageResourceName = nameof(BoardGame_CreateAndUpdateGame.RequiredPrice_ErrorMessage))]
        [Price(
            ErrorMessageResourceType = typeof(BoardGame_CreateAndUpdateGame),
            ErrorMessageResourceName = nameof(BoardGame_CreateAndUpdateGame.Price_ValidationErrorMessage))]
        public double? Price { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(BoardGame_CreateAndUpdateGame),
            ErrorMessageResourceName = nameof(BoardGame_CreateAndUpdateGame.RequiredProductCode_ErrorMessage))]
        [ProductCode(
            ErrorMessageResourceType = typeof(BoardGame_CreateAndUpdateGame),
            ErrorMessageResourceName = nameof(BoardGame_CreateAndUpdateGame.ProductCode_ValidationErrorMessage))]
        public long? ProductCode { get; set; }
    }
}
