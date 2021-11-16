using MessagesManager.Domain;

namespace MessagesManager.Converters
{
    public interface IToUpdateMessage
    {
        MessageModel SetChanges(MessageModel oldMessage, MessageModel newMessage);
    }
}