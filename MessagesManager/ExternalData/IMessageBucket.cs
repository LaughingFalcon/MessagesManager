using MessagesManager.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagesManager.ExternalData
{
    public interface IMessageBucket
    {
        Task<MessageModel> Find(MessageFilter filter);
        Task<List<SimpleMessageModel>> List(MessageFilter filter);
        Task<bool> Save(MessageModel messageModel);
        Task<bool> Update(MessageModel messageModel);
    }
}