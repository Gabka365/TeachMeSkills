using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Models.VideoSort;

namespace PortalAboutEverything.Controllers;

public class VideoSortController : Controller
{
    private readonly VideoSortRepository _videoSortRepository;

    public VideoSortController(VideoSortRepository videoSortRepository)
    {
        _videoSortRepository = videoSortRepository;
    }

    [HttpGet]
    public IActionResult Chat()
    {
        var messages = _videoSortRepository.GetAllMessages().Select(GenerateChatMessageViewModel).ToList();

        return View(messages);
    }

    [HttpPost]
    public IActionResult Chat(string username, string message)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(message)) return RedirectToAction("Chat");

        var newMessage = new ChatMessage
        {
            Username = username,
            Message = message,
            Timestamp = DateTime.Now
        };
        _videoSortRepository.AddMessage(newMessage);

        return RedirectToAction("Chat");
    }

    [HttpGet]
    public IActionResult ChatMessageUpdate(Guid id)
    {
        var message = _videoSortRepository.GetMessage(id);

        return View(GenerateChatMessageViewModel(message));
    }

    [HttpPost]
    public IActionResult ChatMessageUpdate(Guid id, string message)
    {
        _videoSortRepository.UpdateMessage(id, message);

        return RedirectToAction("Chat");
    }

    [HttpGet]
    public IActionResult ChatMessageDelete(Guid id)
    {
        _videoSortRepository.DeleteMessage(id);

        return RedirectToAction("Chat");
    }

    private ChatMessageViewModel GenerateChatMessageViewModel(ChatMessage chatMessage)
    {
        return new ChatMessageViewModel
        {
            Id = chatMessage.Id,
            Username = chatMessage.Username,
            Message = chatMessage.Message,
            Timestamp = chatMessage.Timestamp.ToString("dd.MM.yyyy HH:mm"),
            IsModified = chatMessage.IsModified
        };
    }
}