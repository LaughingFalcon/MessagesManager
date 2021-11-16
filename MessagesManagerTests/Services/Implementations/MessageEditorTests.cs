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
    public class MessageEditorTests
    {
        private readonly Mock<IMessageBucket> _messageBucket;
        private readonly Mock<IToUpdateMessage> _toUpdateMessage;
        private readonly IMessageService _messageEditor;
        public MessageEditorTests()
        {
            _messageBucket = new Mock<IMessageBucket>(MockBehavior.Strict);
            _messageBucket
                .Setup(x => x.Find(It.Is<MessageFilter>(m => !string.IsNullOrEmpty(m.HashKey) && !string.IsNullOrEmpty(m.RangeKey))))
                .ReturnsAsync(new MessageModel() { Id = Guid.NewGuid().ToString() });
            _messageBucket
                .Setup(x => x.Update(It.Is<MessageModel>(m => !string.IsNullOrEmpty(m.Id))))
                .ReturnsAsync(true);

            _toUpdateMessage = new Mock<IToUpdateMessage>(MockBehavior.Strict);
            _toUpdateMessage
                .Setup(x => x.SetChanges(
                    It.Is<MessageModel>(m => !string.IsNullOrEmpty(m.Id)),
                    It.Is<MessageModel>(n => !string.IsNullOrEmpty(n.Id))))
                .Returns(new MessageModel() { Id = Guid.NewGuid().ToString(), Status = MessagesManager.Enums.MessageStatusEnum.MODIFIED });

            _messageEditor = new MessageEditorService(_messageBucket.Object, _toUpdateMessage.Object);
        }

        [Fact]
        public async Task EditTestAsync()
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

            Assert.IsType<bool>(response.Data);

            bool saved = (bool)response.Data;
            Assert.True(saved);
        }
    }
}
