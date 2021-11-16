using MessagesManager.Converters;
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
    public class MessageDeletorTests
    {
        private readonly Mock<IMessageBucket> _messageBucket;
        private readonly Mock<IToDeletMessage> _toDeletMessage;
        private readonly IMessageService _messageDeletor;
        public MessageDeletorTests()
        {
            _messageBucket = new Mock<IMessageBucket>(MockBehavior.Strict);
            _messageBucket
                .Setup(x => x.Find(It.Is<MessageFilter>(m => !string.IsNullOrEmpty(m.HashKey) && !string.IsNullOrEmpty(m.RangeKey))))
                .ReturnsAsync(new MessageModel() { Id = Guid.NewGuid().ToString() });
            _messageBucket
                .Setup(x => x.Update(It.Is<MessageModel>(m => !string.IsNullOrEmpty(m.Id))))
                .ReturnsAsync(true);

            _toDeletMessage = new Mock<IToDeletMessage>(MockBehavior.Strict);
            _toDeletMessage
                .Setup(x => x.DoChanges(It.Is<MessageModel>(m => !string.IsNullOrEmpty(m.Id))))
                .Returns(new MessageModel() { Id = Guid.NewGuid().ToString(), Status = MessagesManager.Enums.MessageStatusEnum.DELETED });

            _messageDeletor = new MessageDeletorService(_messageBucket.Object, _toDeletMessage.Object);
        }

        [Fact]
        public async Task DeleteTestAsync()
        {
            var actionModel = new ActionModel()
            {
                MessageModel = new MessageModel()
                {
                    Id = Guid.NewGuid().ToString()
                }
            };
            var username = Guid.NewGuid().ToString();

            var response = await _messageDeletor.ExecuteAsync(actionModel, username);

            Assert.Equal(username, response.Username);

            Assert.IsType<bool>(response.Data);

            bool saved = (bool)response.Data;
            Assert.True(saved);
        }
    }
}
