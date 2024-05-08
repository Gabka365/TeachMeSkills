using Microsoft.AspNetCore.Mvc;
using PortalAboutEverything.Models.Ancient;
using PortalAboutEverything.Services.Ancient;

namespace PortalAboutEverything.Controllers;

public class AncientController : Controller
{
    private readonly AncientChatService _chatService;

    public AncientController(AncientChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet]
    public IActionResult Chat()
    {
        return View(new AncientChatViewModel()
        {
            ChatMessages = _chatService.ChatMessages
        });
    }

    [HttpPost]
    public IActionResult Chat(string username, string message)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(message))
        {
            return View(new AncientChatViewModel()
            {
                ChatMessages = _chatService.ChatMessages
            });
        }
        
        var newMessage = new AncientChatMessage()
        {
            Username = username,
            Message = message,
            Timestamp = DateTime.Now
        };
        _chatService.ChatMessages.Add(newMessage);

        return View(new AncientChatViewModel()
        {
            ChatMessages = _chatService.ChatMessages
        });
    }
}