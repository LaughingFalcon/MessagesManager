using MessagesManager.Enums;
using MessagesManager.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesManager.Factories
{
    public class MessageFactory : IMessageFactory
    {
        private readonly IDictionary<MessageActionEnum, Func<IMessageService>> _messageService;
        public MessageFactory(IDictionary<MessageActionEnum, Func<IMessageService>> messageService)
        {
            _messageService = messageService;
        }
        public IMessageService Create(MessageActionEnum messageActionEnum)
        {
            if (_messageService.ContainsKey(messageActionEnum))
                return _messageService[messageActionEnum]();
            throw new ArgumentException($"Message action {messageActionEnum} unknown");
        }
    }
}
