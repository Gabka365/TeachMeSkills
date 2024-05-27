using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.Game
{
    public class GameCreateViewModel
    {
        [Required(ErrorMessage = "Я не верю в игры, у которых нет называния")]
        public string Name { get; set; }
        public string? Description { get; set; }

        [ComputerYear]
        [Display(Name = "Год выпуска игры")]
        public int YearOfRelease { get; set; }
    }
}
