using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.BoardGame
{
    public class BoardGameCreateViewModel
    {
        [Required(ErrorMessage = "Название не может быть пустым")]
        [TextInput(2, 35)]
        [Display(Name = "Название")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Слоган не может быть пустым")]
        [TextInput(2, 50)]
        [Display(Name = "Слоган")]
        public string MiniTitle { get; set; }
        [Required(ErrorMessage = "Описание не может быть пустым")]
        [TextInput(90)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        public string? Essence { get; set; }
        public string? Tags { get; set; }
        [Required(ErrorMessage = "Цена не может быть пустой")]
        [Price]
        public double? Price { get; set; }
        [Required(ErrorMessage = "Код товара не может быть пустым")]
        [ProductCode]
        public long? ProductCode { get; set; }
    }
}
