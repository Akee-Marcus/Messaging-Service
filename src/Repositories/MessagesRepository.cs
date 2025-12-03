// Repositories/MessagesRepository.cs
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
        return _messages
            .Where(m => m.SenderId == userId || m.ReceiverId == userId)
            .OrderByDescending(m => m.Timestamp);
    }

    //  all unread messages for a user
    public IEnumerable<Message> GetUnreadMessages(string userId)
    {
        return _messages
            .Where(m => m.ReceiverId == userId && !m.IsRead)
            .OrderBy(m => m.Timestamp);
    }

    //  mark one message as read
    public bool MarkMessageAsRead(Guid messageId)
    {
        var message = _messages.FirstOrDefault(m => m.Id == messageId);
        if (message is null)
        {
            return false;
        }

        if (!message.IsRead)
        {
            message.IsRead = true;
            message.ReadAt = DateTime.UtcNow;
        }

        return true;
    }

    //  mark all messages in a conversation as read for one user
    public int MarkConversationAsRead(string user1, string user2, string readerId)
    {
        var toMark = _messages.Where(m =>
            ((m.SenderId == user1 && m.ReceiverId == user2) ||
             (m.SenderId == user2 && m.ReceiverId == user1)) &&
            m.ReceiverId == readerId &&
            !m.IsRead);

        int count = 0;
        foreach (var m in toMark)
        {
            m.IsRead = true;
            m.ReadAt = DateTime.UtcNow;
            count++;
        }

        return count;
    }
}
