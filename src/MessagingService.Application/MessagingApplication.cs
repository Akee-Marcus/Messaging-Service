using ChatService.Repositories;
using ChatService.Models;

namespace MessagingService.Application;

public class MessagingApplication
{
    private readonly IMessagesRepository _messaging;    

    public MessagingApplication(IMessagesRepository messaging)
    {
        _messaging = messaging;
    }

    public void AddMessage(Message m)
    {
        _messaging.AddMessage(m);
    }

    public IEnumerable<Message> GetMessagesBetween(string u1, string u2)
    {
        return _messaging.GetMessagesBetween(u1, u2);
    }

    public IEnumerable<Message> GetMessagesForUser(string user)
    {
        return _messaging.GetMessagesForUser(user);
    }
}
