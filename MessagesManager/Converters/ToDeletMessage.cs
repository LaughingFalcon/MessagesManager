using MessagesManager.Domain;
using MessagesManager.Enums;
using MessagesManager.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesManager.Converters
{
    public class ToDeletMessage : IToDeletMessage
    {
        public MessageModel DoChanges(MessageModel messageModel)
        {
            messageModel.Version++;
            messageModel.Status = MessageStatusEnum.DELETED;
            messageModel.Changes.Add(
                new Changes()
                {
                    Date = CurrentDay.GetCurrentDay(),
                    Differences = new Dictionary<string, string>()
                    {
                        { "Status", MessageStatusEnum.DELETED.ToString() }
                    }
                }
            );

            return messageModel;
        }
    }
}
