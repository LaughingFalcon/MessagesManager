using MessagesManager.Enums;
using MessagesManager.Factories;
using MessagesManager.Services.Implementations;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MessagesManagerTests.Factory
{
    public class MessageFactoryTests
    {
        private readonly IMessageCreatorService _messageCreatorService;
        private readonly IMessageDeletorService _messageDeletorService;
        private readonly IMessageEditorService _messageEditorService;
        private readonly IMessageListerService _messageListerService;
        private readonly IMessageRetrieverService _messageRetrieverService;
        private readonly IDictionary<MessageActionEnum, Func<IMessageService>> _messageService;
        private readonly IMessageFactory _messageFactory;
        public MessageFactoryTests()
        {
            _messageCreatorService = new MessageCreatorService(null, null);
            _messageDeletorService = new MessageDeletorService(null, null);
            _messageEditorService = new MessageEditorService(null, null);
            _messageListerService = new MessageListerService(null);
            _messageRetrieverService = new MessageRetrieverService(null);

            _messageService = new Dictionary<MessageActionEnum, Func<IMessageService>>() 
            {
                { MessageActionEnum.CREATE, () => _messageCreatorService},
                { MessageActionEnum.DELETE, () => _messageDeletorService},
                { MessageActionEnum.EDIT, () => _messageEditorService},
                { MessageActionEnum.LIST, () => _messageListerService},
                { MessageActionEnum.RETRIEVE, () => _messageRetrieverService}
            };

            _messageFactory = new MessageFactory(_messageService);
        }

        [Fact]
        public void CreateServiceTest()
        {
            var messageAction = MessageActionEnum.CREATE;
            var response = _messageFactory.Create(messageAction);

            Assert.IsType<MessageCreatorService>(response);
        }

        [Fact]
        public void DeleteServiceTest()
        {
            var messageAction = MessageActionEnum.DELETE;
            var response = _messageFactory.Create(messageAction);

            Assert.IsType<MessageDeletorService>(response);
        }

        [Fact]
        public void EditServiceTest()
        {
            var messageAction = MessageActionEnum.EDIT;
            var response = _messageFactory.Create(messageAction);

            Assert.IsType<MessageEditorService>(response);
        }

        [Fact]
        public void ListServiceTest()
        {
            var messageAction = MessageActionEnum.LIST;
            var response = _messageFactory.Create(messageAction);

            Assert.IsType<MessageListerService>(response);
        }

        [Fact]
        public void RetrieveServiceTest()
        {
            var messageAction = MessageActionEnum.RETRIEVE;
            var response = _messageFactory.Create(messageAction);

            Assert.IsType<MessageRetrieverService>(response);
        }
    }
}
