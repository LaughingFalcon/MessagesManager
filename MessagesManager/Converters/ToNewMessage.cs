using MessagesManager.Domain;
using MessagesManager.Enums;
using MessagesManager.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesManager.Converters
{
    public class ToNewMessage : IToNewMessage
    {
        public MessageModel SetNewModel(MessageModel messageModel, string username)
        {
            var changes = new Changes()
            {
                Date = CurrentDay.GetCurrentDay(),
                Differences = new Dictionary<string, string>()
            };
            messageModel.Id = Guid.NewGuid().ToString();
            messageModel.Version = 1;
            messageModel.Status = MessageStatusEnum.NEW;
            messageModel.Creator = username;
            messageModel.Changes = new List<Changes>();

            changes.Date = CurrentDay.GetCurrentDay();
            changes.Differences.Add("Subject", messageModel.Subject);
            changes.Differences.Add("Content", messageModel.Content);
            changes.Differences.Add("StartDate", messageModel.StartDate);
            changes.Differences.Add("ExpirationDate", messageModel.ExpirationDate);
            changes.Differences.Add("Status", MessageStatusEnum.NEW.ToString());

            messageModel.Changes.Add(changes);

            return messageModel;
        }
    }
}
