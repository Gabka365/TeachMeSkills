using PortalAboutEverything.Models.ValidationAttributes;

namespace PortalAboutEverything.Models.Blog
{
    public class WriteBlogComment
    {
        public int postId { get; set; }

        [CorrectTextFormat]
        public string Text { get; set; }
    }
}
