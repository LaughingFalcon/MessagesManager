using MessagesManager.Enums;
using MessagesManager.Services.Implementations;

namespace MessagesManager.Factories
{
    public interface IMessageFactory
    {
        IMessageService Create(MessageActionEnum messageActionEnum);
    }
}