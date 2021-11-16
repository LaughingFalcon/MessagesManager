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
    public class MessageCreatorTests
    {
        private readonly Mock<IMessageBucket> _messageBucket;
        private readonly Mock<IToNewMessage> _toNewMessage;
        private readonly IMessageService _messageCreator;
        public MessageCreatorTests()
        {
            _messageBucket = new Mock<IMessageBucket>(MockBehavior.Strict);
            _messageBucket
                .Setup(x => x.Save(It.IsNotNull<MessageModel>()))
                .ReturnsAsync(true);

            _toNewMessage = new Mock<IToNewMessage>(MockBehavior.Strict);
            _toNewMessage
                .Setup(x => x.SetNewModel(
                    It.Is<MessageModel>(m => !string.IsNullOrEmpty(m.Id)),
                    It.IsAny<string>()))
                .Returns(new MessageModel());

            _messageCreator = new MessageCreatorService(_messageBucket.Object, _toNewMessage.Object);
        }

        [Fact]
        public async Task CreateTestAsync()
        {
            var actionModel = new ActionModel()
            {
                MessageModel = new MessageModel()
                {
                    Id = Guid.NewGuid().ToString()
                }
            };
            var username = Guid.NewGuid().ToString();

            var response = await _messageCreator.ExecuteAsync(actionModel, username);

            Assert.Equal(username, response.Username);

            Assert.IsType<bool>(response.Data);

            bool saved = (bool)response.Data;
            Assert.True(saved);
        }
    }
}
