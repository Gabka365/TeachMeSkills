namespace PortalAboutEverything.Models.VideoSort;

public class ChatMessageViewModel
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Message { get; set; }
    public string Timestamp { get; set; }
    public bool IsModified { get; set; }
}