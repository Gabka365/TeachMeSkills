using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using PortalAboutEverything.LocalizationResources.BoardGame;

namespace PortalAboutEverything.Models.BoardGame
{
    public class BoardGameCreateReviewViewModel
    {
        public int BoardGameId { get; set; }
        public string BoardGameName { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(BoardGame_CreateAndUpdateReview),
            ErrorMessageResourceName = nameof(BoardGame_CreateAndUpdateReview.RequiredText_ErrorMessage))]
        [TextInput(10, 500)]
        [Display(
            ResourceType = typeof(BoardGame_CreateAndUpdateReview),
            Name = nameof(BoardGame_CreateAndUpdateReview.DisplayText_Name))]
        public string Text { get; set; }
    }
}
