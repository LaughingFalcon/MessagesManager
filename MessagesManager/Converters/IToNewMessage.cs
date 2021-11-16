using MessagesManager.Domain;

namespace MessagesManager.Converters
{
    public interface IToNewMessage
    {
        MessageModel SetNewModel(MessageModel messageModel, string username);
    }
}