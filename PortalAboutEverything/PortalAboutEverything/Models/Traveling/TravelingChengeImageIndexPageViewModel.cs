using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.Traveling
{
    public class TravelingChengeImageIndexPageViewModel
    {
        public string oldImagePath { get; set; }

        [Required(ErrorMessage = "Пожалуйста, загрузите файл.")]
        [FileExtension(new string[] { ".png"}, ErrorMessage = "Недопустимый формат файла.")]
        public IFormFile newImage { get; set; }
    }
}
