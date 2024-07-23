using PortalAboutEverything.Data.Enums;

namespace PortalAboutEverything.Models.Blog
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public IFormFile Cover { get; set; }
        public List<PostIndexViewModel>? Posts { get; set; }
        public bool IsAccessible { get; set; }
        public string Name { get; set; }
        public Language UserLanguage { get; set; }
        public UserRole Role { get; set; }
    }
}
