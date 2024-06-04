using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.BoardGame
{
    public class BoardGameUpdateReviewViewModel
    {
        public int BoardGameId { get; set; }
        public string BoardGameName { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Отзыв не может быть пустым")]
        [TextInput(10, 500)]
        [Display(Name = "Отзыв")]
        public string Text { get; set; }
    }
}
