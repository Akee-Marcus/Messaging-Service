using System;
using System.Linq;
using ChatService.Models;
using ChatService.Repositories;
using Xunit;

namespace MessagingService.Infrastructure.Tests
{
    public class MessagesRepositoryTests
    {
        [Fact]
        public void AddMessage_ShouldStoreMessage()
        {
            // Arrange
            var repo = new MessagesRepository();
            var msg = new Message
            {
                SenderId = "A",
                ReceiverId = "B",
                Content = "Hello"
            };

            // Act
            repo.AddMessage(msg);

            // Assert
            var result = repo.GetMessagesForUser("A").ToList();
            Assert.Single(result);
            Assert.Equal("Hello", result[0].Content);
        }

        [Fact]
        public void GetMessagesBetween_ShouldReturnMessagesInBothDirections()
        {
            // Arrange
            var repo = new MessagesRepository();

            var m1 = new Message { SenderId = "A", ReceiverId = "B", Content = "Hi", Timestamp = DateTime.UtcNow.AddMinutes(-2) };
            var m2 = new Message { SenderId = "B", ReceiverId = "A", Content = "Hello", Timestamp = DateTime.UtcNow.AddMinutes(-1) };
            var m3 = new Message { SenderId = "A", ReceiverId = "C", Content = "Not included" };

            repo.AddMessage(m1);
            repo.AddMessage(m2);
            repo.AddMessage(m3);

            // Act
            var result = repo.GetMessagesBetween("A", "B").ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Hi", result[0].Content);     // earlier timestamp first
            Assert.Equal("Hello", result[1].Content);  // later timestamp
        }

        [Fact]
        public void GetMessagesBetween_ShouldReturnInChronologicalOrder()
        {
            // Arrange
            var repo = new MessagesRepository();

            var old = new Message { SenderId = "A", ReceiverId = "B", Content = "Old", Timestamp = DateTime.UtcNow.AddMinutes(-10) };
            var middle = new Message { SenderId = "B", ReceiverId = "A", Content = "Mid", Timestamp = DateTime.UtcNow.AddMinutes(-5) };
            var newest = new Message { SenderId = "A", ReceiverId = "B", Content = "New", Timestamp = DateTime.UtcNow.AddMinutes(-1) };

            repo.AddMessage(old);
            repo.AddMessage(middle);
            repo.AddMessage(newest);

            // Act
            var result = repo.GetMessagesBetween("A", "B").ToList();

            // Assert
            Assert.Equal(new[] { "Old", "Mid", "New" }, result.Select(m => m.Content).ToArray());
        }

        [Fact]
        public void GetMessagesForUser_ShouldReturnBothSentAndReceived()
        {
            // Arrange
            var repo = new MessagesRepository();

            var m1 = new Message { SenderId = "A", ReceiverId = "B", Content = "Sent" };
            var m2 = new Message { SenderId = "B", ReceiverId = "A", Content = "Received" };
            var m3 = new Message { SenderId = "C", ReceiverId = "D", Content = "Not included" };

            repo.AddMessage(m1);
            repo.AddMessage(m2);
            repo.AddMessage(m3);

            // Act
            var result = repo.GetMessagesForUser("A").ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, m => m.Content == "Sent");
            Assert.Contains(result, m => m.Content == "Received");
        }

        [Fact]
        public void GetMessagesForUser_ShouldReturnDescendingTimestamp()
        {
            // Arrange
            var repo = new MessagesRepository();

            var old = new Message { SenderId = "A", ReceiverId = "B", Content = "Old", Timestamp = DateTime.UtcNow.AddMinutes(-10) };
            var mid = new Message { SenderId = "B", ReceiverId = "A", Content = "Mid", Timestamp = DateTime.UtcNow.AddMinutes(-5) };
            var newest = new Message { SenderId = "A", ReceiverId = "B", Content = "New", Timestamp = DateTime.UtcNow.AddMinutes(-1) };

            repo.AddMessage(old);
            repo.AddMessage(mid);
            repo.AddMessage(newest);

            // Act
            var result = repo.GetMessagesForUser("A").ToList();

            // Assert — newest first
            Assert.Equal(new[] { "New", "Mid", "Old" }, result.Select(m => m.Content).ToArray());
        }
    }
}
