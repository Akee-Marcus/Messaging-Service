using System.Collections.Generic;
using System.Linq;
using ChatService.Models;
using ChatService.Repositories;
using MessagingService.Application;
using Moq;
using Xunit;

namespace MessagingService.Tests
{
    public class MessagingApplicationTests
    {
        private readonly Mock<IMessagesRepository> _mockRepo;
        private readonly MessagingApplication _app;

        public MessagingApplicationTests()
        {
            _mockRepo = new Mock<IMessagesRepository>();
            _app = new MessagingApplication(_mockRepo.Object);
        }

        [Fact]
        public void AddMessage_ShouldCallRepository()
        {
            // Arrange
            var message = new Message
            {
                SenderId = "Alice",
                ReceiverId = "Bob",
                Content = "Hello!"
            };

            // Act
            _app.AddMessage(message);

            // Assert
            _mockRepo.Verify(r => r.AddMessage(It.IsAny<Message>()), Times.Once);
        }

        [Fact]
        public void GetMessagesBetween_ShouldReturnRepositoryResults()
        {
            // Arrange
            var expectedMessages = new List<Message>
            {
                new Message { SenderId = "Alice", ReceiverId = "Bob", Content = "Hi" },
                new Message { SenderId = "Bob", ReceiverId = "Alice", Content = "Hello" }
            };

            _mockRepo
                .Setup(r => r.GetMessagesBetween(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(expectedMessages);

            // Act
            var result = _app.GetMessagesBetween("Alice", "Bob").ToList();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetMessagesForUser_ShouldReturnRepositoryResults()
        {
            // Arrange
            var expectedMessages = new List<Message>
            {
                new Message { SenderId = "Alice", ReceiverId = "Bob", Content = "Hi" },
                new Message { SenderId = "Charlie", ReceiverId = "Alice", Content = "Hey" }
            };

            _mockRepo
                .Setup(r => r.GetMessagesForUser(It.IsAny<string>()))
                .Returns(expectedMessages);

            // Act
            var result = _app.GetMessagesForUser("Alice").ToList();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}
