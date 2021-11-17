using MessagesManager.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesManager.Domain
{
    public class SimpleMessageModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        [JsonProperty("expirationDate")]
        public string ExpirationDate { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("status")]
        public MessageStatusEnum Status { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }
    }
}
