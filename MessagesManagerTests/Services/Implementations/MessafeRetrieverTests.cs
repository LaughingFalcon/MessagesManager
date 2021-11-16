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
    public class MessafeRetrieverTests
    {
        private readonly Mock<IMessageBucket> _messageBucket;
        private readonly IMessageService _messageRetriever;
        public MessafeRetrieverTests()
        {
            _messageBucket = new Mock<IMessageBucket>(MockBehavior.Strict);
            _messageBucket
                .Setup(x => x.Find(It.Is<MessageFilter>(m => !string.IsNullOrEmpty(m.HashKey) && !string.IsNullOrEmpty(m.RangeKey))))
                .ReturnsAsync(new MessageModel());

            _messageRetriever = new MessageRetrieverService(_messageBucket.Object);
        }

        [Fact]
        public async Task RetrieverTestAsync()
        {
            var actionModel = new ActionModel()
            {
                MessageModel = new MessageModel()
                {
                    Id = Guid.NewGuid().ToString()
                }
            };
            var username = Guid.NewGuid().ToString();

            var response = await _messageRetriever.ExecuteAsync(actionModel, username);

            Assert.Equal(username, response.Username);

            Assert.IsType<MessageModel>(response.Data);
        }
    }
}
