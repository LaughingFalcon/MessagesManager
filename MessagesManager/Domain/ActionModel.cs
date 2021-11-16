using MessagesManager.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesManager.Domain
{
    public class ActionModel
    {
        [JsonProperty("action")]
        public MessageActionEnum Action { get; set; }

        [JsonProperty("messageModel")]
        public MessageModel MessageModel { get; set; }
    }
}
