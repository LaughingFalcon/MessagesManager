using MessagesManager.Converters;
using MessagesManager.Domain;
using MessagesManager.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MessagesManagerTests.Converters
{
    public class ToDeleteMessageTests
    {
        private readonly IToDeletMessage _toDeletMessage;
        public ToDeleteMessageTests()
        {
            _toDeletMessage = new ToDeletMessage();
        }

        [Fact]
        public void DoChangesTest()
        {
            var baseVerson = 1;
            var expectVersion = 2;
            var expectStatus = MessageStatusEnum.DELETED;
            var expectKey = "Status";

            var model = new MessageModel()
            {
                Version = baseVerson,
                Changes = new List<Changes>()
            };

            var response = _toDeletMessage.DoChanges(model);

            Assert.NotNull(response);

            Assert.Equal(expectVersion, response.Version);
            Assert.Equal(expectStatus, response.Status);

            Assert.NotNull(response.Changes);
            Assert.NotEmpty(response.Changes);

            Assert.True(response.Changes.First().Differences.ContainsKey(expectKey));
            Assert.Equal(expectStatus.ToString(), response.Changes.First().Differences[expectKey]);
        }
    }
}
