using Microsoft.AspNetCore.Mvc;
using ChatService.Models;
using ChatService.Repositories;

namespace ChatService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly MessagesRepository _repository;

    public MessagesController(MessagesRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult SendMessage([FromBody] Message message)
    {
        if (string.IsNullOrWhiteSpace(message.SenderId) ||
            string.IsNullOrWhiteSpace(message.ReceiverId) ||
            string.IsNullOrWhiteSpace(message.Content))
        {
            return BadRequest("SenderId, ReceiverId, and Content are required.");
        }

        _repository.AddMessage(message);
        return Ok(message);
    }

    [HttpGet("{user1}/{user2}")]
    public IActionResult GetConversation(string user1, string user2)
    {
        var messages = _repository.GetMessagesBetween(user1, user2);
        return Ok(messages);
    }

    [HttpGet("{userId}")]
    public IActionResult GetUserMessages(string userId)
    {
        var messages = _repository.GetMessagesForUser(userId);
        return Ok(messages);
    }
}
