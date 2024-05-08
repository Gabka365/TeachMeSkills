using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Ancient;
using PortalAboutEverything.Services.Ancient;

namespace PortalAboutEverything.Controllers;

public class VideoSortController : Controller
{
    private readonly VideoSortChatService _chatService;

    public VideoSortController(VideoSortChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet]
    public IActionResult Chat()
    {
        return View(new VideoSortChatViewModel()
        {
            ChatMessages = _chatService.ChatMessages
        });
    }

    [HttpPost]
    public IActionResult Chat(string username, string message)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(message))
        {
            return View(new VideoSortChatViewModel()
            {
                ChatMessages = _chatService.ChatMessages
            });
        }
        
        var newMessage = new VideoSortChatMessage()
        {
            Username = username,
            Message = message,
            Timestamp = DateTime.Now
        };
        _chatService.ChatMessages.Add(newMessage);

        return View(new VideoSortChatViewModel()
        {
            ChatMessages = _chatService.ChatMessages
        });
    }
}