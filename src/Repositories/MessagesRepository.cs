using ChatService.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChatService.Repositories
{
    public class MessagesRepository
    {
        private readonly List<Message> _messages = new();

        public void AddMessage(Message message)
        {
            _messages.Add(message);
        }

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
                            .OrderBy(m => m.Timestamp);
        }

        public IEnumerable<Message> GetUnreadMessages(string userId)
        {
            return _messages.Where(m => m.ReceiverId == userId && !m.IsRead)
                            .OrderBy(m => m.Timestamp);
        }

        // Optional: Mark messages as read
        public void MarkAsRead(string userId)
        {
            foreach (var msg in _messages.Where(m => m.ReceiverId == userId && !m.IsRead))
            {
                msg.IsRead = true;
            }
        }
    }
}
