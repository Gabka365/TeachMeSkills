using PortalAboutEverything.Models.ValidationAttributes;

namespace PortalAboutEverything.Models.Store
{
    public class GoodUpdateViewModel
    {
        public int Id { get; set; }
        [GoodLenthRestriction(5,20)]
        [GoodNoSpecialCharacters]
        public string? Name { get; set; }
        [GoodLenthRestriction(10, 30)]
        [GoodNoSpecialCharacters]
        public string? Description { get; set; }
        [GoodPrice]
        [GoodNoSpecialCharacters]
        public int? Price { get; set; }
    }
}
