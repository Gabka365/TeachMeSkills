using PortalAboutEverything.Models.ValidationAttributes;

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

        [GoodPriceIsNotEmpty]
        [GoodNoSpecialCharacters]
        public int? Price { get; set; }

        public List<AddGoodReviewViewModel>? Reviews { get; set; }
        
        public IFormFile? Cover { get; set; }

        public bool HasCover { get; set; }
    }
}
