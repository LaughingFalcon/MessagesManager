using MessagesManager;
using MessagesManager.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MessagesManagerTests.Integration
{
    public class FunctionTests
    {
        private readonly Function _function;
        public FunctionTests()
        {
            _function = new Function();
        }

        [Fact]
        public async Task MainTest()
        {
            var expectUsername = "username";
            var userModel = new UserModel()
            {
                Username = expectUsername,
                Password = "password",
                Token = "dG9rZW4="
            };

            var bytesArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userModel));
            var authEncoded = Convert.ToBase64String(bytesArray);
            var actionModel = new ActionModel()
            {
                Action = MessagesManager.Enums.MessageActionEnum.CREATE,
                MessageModel = new MessageModel()
                {
                    Content = "Content",
                    ExpirationDate = "01/11/2021",
                    StartDate = "30/11/2021",
                    Subject = "Subject",
                }
            };
            var actionJson = JsonConvert.SerializeObject(actionModel);

            var response = await _function.Main(authEncoded, actionJson);

            Assert.NotNull(response);
            Assert.IsType<bool>(response.Data);
            Assert.True((bool)response.Data);
            Assert.Equal(expectUsername, response.Username);
        }
}
}
