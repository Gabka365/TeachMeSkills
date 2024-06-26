using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.Traveling
{
    public class TravelingCreateViewModel
    {
        [Required(ErrorMessage = "Название не должно быть пустым")]
        [TravelingName]
        public string Name { get; set; }

        [Required(ErrorMessage = "Описание не должно быть пустым")]
        [ProfanityFilter]
        public string Desc { get; set; }

        public string TimeOfCreation { get; set; } = DateTime.Now.ToString("dd MMM yyyy");

        [Required(ErrorMessage = "Пожалуйста, загрузите файл.")]
        [FileExtension(new string[] { ".png", ".jpg", ".jpeg", ".gif" }, ErrorMessage = "Недопустимый формат файла.")]
        public IFormFile Image { get; set; }

    }
}
