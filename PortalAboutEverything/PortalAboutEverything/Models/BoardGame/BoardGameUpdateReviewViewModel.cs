using PortalAboutEverything.LocalizationResources.BoardGame;
using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.BoardGame
{
    public class BoardGameUpdateReviewViewModel
    {
        public int BoardGameId { get; set; }
        public string BoardGameName { get; set; }
        public int Id { get; set; }

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
