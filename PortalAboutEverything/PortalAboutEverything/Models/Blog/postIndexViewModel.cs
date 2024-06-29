namespace PortalAboutEverything.Models.Blog
{
    public class PostIndexViewModel
    {
        public int Id { get; set; }

        public string? Message { get; set; }

        public string? Name { get; set; }

        public DateTime? CurrentTime { get; set; }

        public List<BlogCommentViewModel> CommentsBlog { get; set; }
    }
}
