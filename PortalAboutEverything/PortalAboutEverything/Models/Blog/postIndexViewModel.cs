namespace PortalAboutEverything.Models.Blog
{
    public class PostIndexViewModel
    {
        public int Id { get; set; }

        public string? Message { get; set; }

        public string? Name { get; set; }

        public string DateOfPublish { get; set; }

        public DateTime? CurrentTime { get; set; }

        public bool HasCover { get; set; }

        public List<BlogCommentViewModel> CommentsBlog { get; set; }
    }
}
