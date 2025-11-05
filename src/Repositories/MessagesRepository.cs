using ChatService.Models;
using System.Collections.Concurrent;
namespace ChatService.Repositories;

public class MessagesRepository
{
    private readonly ConcurrentBag<Message> _messages = new();

    public void AddMessage(Message message) => _messages.Add(message);

    public IEnumerable<Message> GetMessagesBetween(string user1, string user2)
    {
        return _messages.Where(m =>
            (m.SenderId == user1 && m.ReceiverId == user2) ||
            (m.SenderId == user2 && m.ReceiverId == user1))
            .OrderBy(m => m.Timestamp);
    }

    public IEnumerable<Message> GetMessagesForUser(string userId)
    {
        return _messages.Where(m => m.SenderId == userId || m.ReceiverId == userId)
                        .OrderByDescending(m => m.Timestamp);
    }
}

