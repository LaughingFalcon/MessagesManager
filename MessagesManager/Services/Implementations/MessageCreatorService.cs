using MessagesManager.Converters;
using MessagesManager.Domain;
using MessagesManager.ExternalData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessagesManager.Services.Implementations
{
    public class MessageCreatorService : IMessageCreatorService
    {
        private readonly IMessageBucket _messageBucket;
        private readonly IToNewMessage _toNewMessage;
        public MessageCreatorService(IMessageBucket messageBucket, IToNewMessage toNewMessage)
        {
            _messageBucket = messageBucket;
            _toNewMessage = toNewMessage;
        }
        public async Task<ResponseModel> ExecuteAsync(ActionModel actionModel, string username)
        {
            actionModel.MessageModel = _toNewMessage.SetNewModel(actionModel.MessageModel, username);
            var saved = await _messageBucket.Save(actionModel.MessageModel);

            var response = new ResponseModel()
            {
                Username = username,
                Data = saved
            };
            return response;
        }
    }
}
