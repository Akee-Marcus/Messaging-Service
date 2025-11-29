using System;
using ChatService.Models;
using Xunit;

namespace MessagingService.Tests
{
    public class MessageTests
    {
        [Fact]
        public void Message_ShouldAutoGenerateId()
        {
            var m1 = new Message();
            var m2 = new Message();

            Assert.NotEqual(Guid.Empty, m1.Id);
            Assert.NotEqual(Guid.Empty, m2.Id);
            Assert.NotEqual(m1.Id, m2.Id);
        }

        [Fact]
        public void Message_ShouldAutoGenerateTimestamp()
        {
            var msg = new Message();

            Assert.True((DateTime.UtcNow - msg.Timestamp).TotalSeconds < 2);
        }

        [Fact]
        public void Message_ShouldStoreProperties()
        {
            var msg = new Message
            {
                SenderId = "UserA",
                ReceiverId = "UserB",
                Content = "Hello!"
            };

            Assert.Equal("UserA", msg.SenderId);
            Assert.Equal("UserB", msg.ReceiverId);
            Assert.Equal("Hello!", msg.Content);
        }

        [Fact]
        public void Message_PropertiesShouldBeSettable()
        {
            var id = Guid.NewGuid();
            var time = new DateTime(2020, 1, 1);

            var msg = new Message
            {
                Id = id,
                Timestamp = time,
                SenderId = "A",
                ReceiverId = "B",
                Content = "Test"
            };

            Assert.Equal(id, msg.Id);
            Assert.Equal("A", msg.SenderId);
            Assert.Equal("B", msg.ReceiverId);
            Assert.Equal("Test", msg.Content);
            Assert.Equal(time, msg.Timestamp);
        }
    }
}
