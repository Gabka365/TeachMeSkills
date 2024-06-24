using PortalAboutEverything.LocalizationResources;
using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.Traveling
{
    public class TravelingCreateComment
    {
       
        [Required(
            ErrorMessageResourceType = typeof(Traveling_Index),
            ErrorMessageResourceName = nameof(Traveling_Index.CommentErrorMessage))]
        [AntiSpam]
        public string Text { get; set; }

        public int TravelingId { get; set; }
    }
}
