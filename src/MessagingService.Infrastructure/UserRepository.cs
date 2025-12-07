using System.Collections.Concurrent;
using ChatService.Models;

namespace ChatService.Repositories
{
    public class UsersRepository
    {
        private readonly ConcurrentDictionary<string, User> _users = new();

        public UsersRepository()
        {
            // Seed a couple of users so you can log in immediately
            AddUser(new User { UserName = "alice", Password = "password1" });
            AddUser(new User { UserName = "bob",   Password = "password2" });
        }

        public void AddUser(User user)
        {
            // Keyed by username
            _users[user.UserName] = user;
        }

        public User? ValidateUser(string userName, string password)
        {
            if (_users.TryGetValue(userName, out var user) &&
                user.Password == password)
            {
                return user;
            }

            return null;
        }

        public IEnumerable<User> GetAllUsers() => _users.Values;
    }
}
