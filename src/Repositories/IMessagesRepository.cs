namespace ChatService.Repositories
{
    public interface IMessagesRepository
    {
        void AddMessage(Message message);
        IEnumerable<Message> GetMessagesBetween(string user1, string user2);
        IEnumerable<Message> GetMessagesForUser(string userId);
    }
}
