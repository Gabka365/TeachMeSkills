using PortalAboutEverything.Models.ValidationAttributes;

namespace PortalAboutEverything.Models.Store
{
    public class GoodUpdateViewModel
    {
        public int Id { get; set; }
        [GoodName]
        public string? Name { get; set; }
        [GoodDescription]
        public string? Description { get; set; }
        [GoodPrice]
        public int Price { get; set; }
    }
}
