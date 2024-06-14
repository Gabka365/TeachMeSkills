using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.GameStore
{
    public class CreateGameStoreViewModel
    {
        [Required(ErrorMessage = "Нужно ввести Название игры")]
        public string GameName { get; set; }

        [Required(ErrorMessage = "У игры обязательно есть разработчик")]
        public string Developer { get; set; }

        [ComputerGameYear(ErrorMessage = "Нужно ввести год релиза игры")]
        [Display(Name = "Год выпуска игры")]
        public int YearOfRelease { get; set; }

        public IFormFile? Cover { get; set; }
    }
}
