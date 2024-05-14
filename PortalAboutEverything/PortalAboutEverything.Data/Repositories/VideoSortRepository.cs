using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories;

public class VideoSortRepository
{
    private readonly List<ChatMessage> _chatMessages = [];

    public List<ChatMessage> GetAllMessages()
    {
        return _chatMessages;
    }

    public ChatMessage GetMessage(Guid id)
    {
        return _chatMessages.Single(message => message.Id == id);
    }

    public void AddMessage(ChatMessage message)
    {
        message.Id = Guid.NewGuid();
        message.IsModified = false;
        _chatMessages.Add(message);
    }

    public void UpdateMessage(Guid id, string text)
    {
        var message = _chatMessages.Single(message => message.Id == id);
        message.Message = text;
        message.IsModified = true;
    }

    public void DeleteMessage(Guid id)
    {
        var message = _chatMessages.Single(message => message.Id == id);
        _chatMessages.Remove(message);
    }
}