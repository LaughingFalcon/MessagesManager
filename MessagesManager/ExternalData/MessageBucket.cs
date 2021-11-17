using MessagesManager.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessagesManager.ExternalData
{
    public class MessageBucket : IMessageBucket
    {
        public async Task<MessageModel> Find(MessageFilter filter)
        {
            return await Task.FromResult(new MessageModel());
        }

        public async Task<List<SimpleMessageModel>> List(MessageFilter filter)
        {
            return await Task.FromResult(new List<SimpleMessageModel>());
        }

        public async Task<bool> Save(MessageModel messageModel)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> Update(MessageModel messageModel)
        {
            return await Task.FromResult(!(messageModel is null));
        }
    }
}
