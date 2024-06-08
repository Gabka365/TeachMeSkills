using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.Store
{
    public class GoodViewModel
    {
        public int Id { get; set; }

        [GoodLenthRestriction(5, 20)]
        [GoodNoSpecialCharacters]
        public string? Name { get; set; }

        [GoodLenthRestriction(10, 30)]
        [GoodNoSpecialCharacters]
        public string? Description { get; set; }

        [GoodPrice]
        [GoodNoSpecialCharacters]
        public int? Price { get; set; }

        public List<AddGoodReviewViewModel>? Reviews { get; set; }
    }
}
