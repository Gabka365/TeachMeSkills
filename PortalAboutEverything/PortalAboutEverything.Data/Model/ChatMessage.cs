namespace PortalAboutEverything.Data.Model;

public class ChatMessage
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsModified { get; set; }
}