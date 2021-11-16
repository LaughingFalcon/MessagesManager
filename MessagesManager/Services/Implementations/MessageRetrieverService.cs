using MessagesManager.Domain;
using MessagesManager.ExternalData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessagesManager.Services.Implementations
{
    public class MessageRetrieverService : IMessageRetrieverService
    {
        private readonly IMessageBucket _messageBucket;
        public MessageRetrieverService(IMessageBucket messageBucket)
        {
            _messageBucket = messageBucket;
        }
        public async Task<ResponseModel> ExecuteAsync(ActionModel actionModel, string username)
        {
            var filter = new MessageFilter()
            {
                HashKey = actionModel.MessageModel.Id,
                RangeKey = username
            };
            var message = await _messageBucket.Find(filter);

            var response = new ResponseModel()
            {
                Username = username,
                Data = message
            };
            return response;
        }
    }
}
