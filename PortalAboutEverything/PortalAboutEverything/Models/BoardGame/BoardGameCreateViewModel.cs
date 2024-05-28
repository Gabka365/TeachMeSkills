using PortalAboutEverything.Models.ValidationAttributes;

namespace PortalAboutEverything.Models.BoardGame
{
    public class BoardGameCreateViewModel
    {
        public string Title { get; set; }
        public string MiniTitle { get; set; }
        public string Description { get; set; }
        public string Essence { get; set; }
        public string Tags { get; set; }
        [Price]
        public double Price { get; set; }
        [ProductCode]
        public long ProductCode { get; set; }
    }
}
