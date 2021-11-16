using MessagesManager.Converters;
using MessagesManager.Domain;
using MessagesManager.ExternalData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessagesManager.Services.Implementations
{
    public class MessageEditorService : IMessageEditorService
    {
        private readonly IMessageBucket _messageBucket;
        private readonly IToUpdateMessage _toUpdateMessage;
        public MessageEditorService(IMessageBucket messageBucket, IToUpdateMessage toUpdateMessage)
        {
            _messageBucket = messageBucket;
            _toUpdateMessage = toUpdateMessage;
        }
        public async Task<ResponseModel> ExecuteAsync(ActionModel actionModel, string username)
        {
            var filter = new MessageFilter()
            {
                HashKey = actionModel.MessageModel.Id,
                RangeKey = username
            };
            var messageToUpdate = await _messageBucket.Find(filter);
            messageToUpdate = _toUpdateMessage.SetChanges(messageToUpdate, actionModel.MessageModel);
            var updated = await _messageBucket.Update(messageToUpdate);

            var response = new ResponseModel()
            {
                Username = username,
                Data = updated
            };
            return response;
        }
    }
}
