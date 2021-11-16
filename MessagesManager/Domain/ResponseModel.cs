using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesManager.Domain
{
    public class ResponseModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }
    }
}
