using MessagesManager.Domain;
using MessagesManager.ExternalData;
using MessagesManager.Services.Implementations;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MessagesManagerTests.Services.Implementations
{
    public class MessageListerTests
    {
        private readonly Mock<IMessageBucket> _messageBucket;
        private readonly IMessageService _messageEditor;
        public MessageListerTests()
        {
            _messageBucket = new Mock<IMessageBucket>(MockBehavior.Strict);
            _messageBucket
                .Setup(x => x.List(It.Is<MessageFilter>(m => !string.IsNullOrEmpty(m.RangeKey))))
                .ReturnsAsync(new List<MessageModel>());

            _messageEditor = new MessageListerService(_messageBucket.Object);
        }

        [Fact]
        public async Task ListTestAsync()
        {
            var actionModel = new ActionModel()
            {
                MessageModel = new MessageModel()
                {
                    Id = Guid.NewGuid().ToString()
                }
            };
            var username = Guid.NewGuid().ToString();

            var response = await _messageEditor.ExecuteAsync(actionModel, username);

            Assert.Equal(username, response.Username);

            Assert.IsType<List<MessageModel>>(response.Data);
        }
    }
}
