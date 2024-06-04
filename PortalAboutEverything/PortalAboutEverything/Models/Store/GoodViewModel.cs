using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.Store
{
    public class GoodViewModel
    {
        public int Id { get; set; }

        [GoodName]
        public string? Name { get; set; }

        [GoodDescription]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполнено.")]
        [GoodPrice]       
        public int Price { get; set; }

        public List<AddGoodReviewViewModel>? Reviews { get; set; }
    }
}
