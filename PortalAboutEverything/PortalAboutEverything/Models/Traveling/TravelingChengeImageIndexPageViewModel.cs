using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.Traveling
{
    public class TravelingChengeImageIndexPageViewModel
    {
        public string OldImagePath { get; set; }

        [Required(ErrorMessage = "Пожалуйста, загрузите файл.")]
        [FileExtension( ".png", ErrorMessage = "Недопустимый формат файла.")]
        public IFormFile NewImage { get; set; }
    }
}
