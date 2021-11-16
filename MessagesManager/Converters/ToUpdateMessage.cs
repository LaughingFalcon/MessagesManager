using MessagesManager.Domain;
using MessagesManager.Enums;
using MessagesManager.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesManager.Converters
{
    public class ToUpdateMessage : IToUpdateMessage
    {
        public MessageModel SetChanges(MessageModel oldMessage, MessageModel newMessage)
        {
            var key = string.Empty; bool hasChanges;
            var changes = new Changes()
            {
                Date = CurrentDay.GetCurrentDay(),
                Differences = new Dictionary<string, string>()
            };

            key = !(oldMessage.Subject.Equals(newMessage.Subject)) ? "Subject" : string.Empty;
            changes.Differences.TryAdd(key, newMessage.Subject);

            key = !(oldMessage.Content.Equals(newMessage.Content)) ? "Content" : string.Empty;
            changes.Differences.TryAdd(key, newMessage.Content);

            key = !(oldMessage.StartDate.Equals(newMessage.StartDate)) ? "StartDate" : string.Empty;
            changes.Differences.TryAdd(key, newMessage.StartDate);

            key = !(oldMessage.ExpirationDate.Equals(newMessage.ExpirationDate)) ? "ExpirationDate" : string.Empty;
            changes.Differences.TryAdd(key, newMessage.ExpirationDate);

            key = !(oldMessage.Status.Equals(MessageStatusEnum.MODIFIED)) ? "Status" : string.Empty;
            changes.Differences.TryAdd(key, MessageStatusEnum.MODIFIED.ToString());

            changes.Differences.Remove(string.Empty);

            hasChanges = changes.Differences.Count > 0;

            newMessage.Version = oldMessage.Version + 1;
            newMessage.Status = MessageStatusEnum.MODIFIED;
            newMessage.Creator = oldMessage.Creator;
            newMessage.Changes = oldMessage.Changes;
            newMessage.Changes.Add(changes);

            if(hasChanges)
                return newMessage;
            return null;
        }
    }
}
