using MessagesManager.Domain;

namespace MessagesManager.Converters
{
    public interface IToDeletMessage
    {
        MessageModel DoChanges(MessageModel oldMessageModel);
    }
}