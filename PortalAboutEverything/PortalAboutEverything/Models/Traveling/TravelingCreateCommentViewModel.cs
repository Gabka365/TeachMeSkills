using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.Traveling
{
    public class TravelingCreateComment
    {
        [Required(ErrorMessage = "Комментарии не должны быть пустыми")]
        [AntiSpam]
        public string Text { get; set; }
    }
}
