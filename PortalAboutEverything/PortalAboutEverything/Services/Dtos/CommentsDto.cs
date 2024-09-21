namespace PortalAboutEverything.Services.Dtos
{
    public class CommentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string CurrentTime { get; set; }
        public int PostId { get; set; }
    }
}
