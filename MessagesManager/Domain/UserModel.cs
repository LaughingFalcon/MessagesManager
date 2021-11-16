using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesManager.Domain
{
    public class UserModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
