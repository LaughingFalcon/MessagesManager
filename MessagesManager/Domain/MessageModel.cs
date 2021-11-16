using System;
using System.Collections.Generic;
using System.Text;
using MessagesManager.Enums;
using Newtonsoft.Json;

namespace MessagesManager.Domain
{
    public class MessageModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        [JsonProperty("expirationDate")]
        public string ExpirationDate { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("status")]
        public MessageStatusEnum Status { get; set; }

        [JsonProperty("changes")]
        public List<Changes> Changes { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }
    }

    public partial class Changes
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("differences")]
        public Dictionary<string,string> Differences { get; set; }
    }
}
