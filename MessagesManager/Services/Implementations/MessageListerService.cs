using MessagesManager.Domain;
using MessagesManager.ExternalData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessagesManager.Services.Implementations
{
    public class MessageListerService : IMessageListerService
    {
        private readonly IMessageBucket _messageBucket;
        public MessageListerService(IMessageBucket messageBucket)
        {
            _messageBucket = messageBucket;
        }
        public async Task<ResponseModel> ExecuteAsync(ActionModel actionModel, string username)
        {
            var filter = new MessageFilter()
            {
                RangeKey = username
            };
            var listMessage = await _messageBucket.List(filter);

            var response = new ResponseModel()
            {
                Username = username,
                Data = listMessage
            };
            return response;
        }
    }
}
