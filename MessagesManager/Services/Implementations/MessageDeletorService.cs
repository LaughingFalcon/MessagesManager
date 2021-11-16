using MessagesManager.Converters;
using MessagesManager.Domain;
using MessagesManager.Enums;
using MessagesManager.ExternalData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessagesManager.Services.Implementations
{
    public class MessageDeletorService : IMessageDeletorService
    {
        private readonly IMessageBucket _messageBucket;
        private readonly IToDeletMessage _toDeletMessage;
        public MessageDeletorService(IMessageBucket messageBucket, IToDeletMessage toDeletMessage)
        {
            _messageBucket = messageBucket;
            _toDeletMessage = toDeletMessage;
        }
        public async Task<ResponseModel> ExecuteAsync(ActionModel actionModel, string username)
        {
            var filter = new MessageFilter()
            {
                HashKey = actionModel.MessageModel.Id,
                RangeKey = username
            };
            var messageToDelete = await _messageBucket.Find(filter);
            messageToDelete = _toDeletMessage.DoChanges(messageToDelete);
            var deleted = await _messageBucket.Update(messageToDelete);

            var response = new ResponseModel()
            {
                Username = username,
                Data = deleted
            };
            return response;
        }
    }
}
