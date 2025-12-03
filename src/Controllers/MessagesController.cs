// Controllers/MessagesController.cs
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

        // make sure server sets these
        message.Timestamp = DateTime.UtcNow;
        message.IsRead = false;
        message.ReadAt = null;

        _repository.AddMessage(message);
        return Ok(message);
    }

    [HttpGet("conversation/{user1}/{user2}")]
    public IActionResult GetConversation(string user1, string user2)
    {
        var messages = _repository.GetMessagesBetween(user1, user2);
        return Ok(messages);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetUserMessages(string userId)
    {
        var messages = _repository.GetMessagesForUser(userId);
        return Ok(messages);
    }

    //  get unread messages for a user
    [HttpGet("unread/{userId}")]
    public IActionResult GetUnreadMessages(string userId)
    {
        var messages = _repository.GetUnreadMessages(userId);
        return Ok(messages);
    }

    //  mark a single message as read (read receipt)
    [HttpPut("{messageId:guid}/read")]
    public IActionResult MarkMessageAsRead(Guid messageId)
    {
        var success = _repository.MarkMessageAsRead(messageId);
        if (!success)
        {
            return NotFound($"Message with id {messageId} not found.");
        }

        return NoContent();
    }

    //  mark an entire conversation as read for one user
    [HttpPut("conversation/{user1}/{user2}/read/{readerId}")]
    public IActionResult MarkConversationAsRead(string user1, string user2, string readerId)
    {
        var count = _repository.MarkConversationAsRead(user1, user2, readerId);
        return Ok(new { updated = count });
    }
}
