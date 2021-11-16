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
    public class ToNewMessageTests
    {
        private readonly IToNewMessage _toNewMessage;
        public ToNewMessageTests()
        {
            _toNewMessage = new ToNewMessage();
        }

        [Fact]
        public void DoChangesTest()
        {
            #region EXPECTS
            var expectVersion = 1;
            var expectKeySubject = "Subject";
            var expectKeyContent = "Content";
            var expectKeyStart = "StartDate";
            var expectKeyExpiration = "ExpirationDate";
            var expectKeyStatus = "Status";
            var expectChanges = 1;
            var expectDiffs = 5;
            var expectSubject = Guid.NewGuid().ToString();
            var expectContent = Guid.NewGuid().ToString();
            var expectStartDate = Guid.NewGuid().ToString();
            var expectExpirationDate = Guid.NewGuid().ToString();
            var expectStatus = MessageStatusEnum.NEW;
            #endregion

            var username = Guid.NewGuid().ToString();
            var model = new MessageModel()
            {
                Subject = expectSubject,
                Content = expectContent,
                StartDate = expectStartDate,
                ExpirationDate = expectExpirationDate
            };

            var response = _toNewMessage.SetNewModel(model, username);

            Assert.NotNull(response);

            Assert.NotEmpty(response.Id);
            Assert.Equal(expectVersion, response.Version);
            Assert.Equal(expectStatus, response.Status);
            Assert.Equal(username, response.Creator);
            
            Assert.NotNull(response.Changes);
            Assert.NotEmpty(response.Changes);
            Assert.Equal(expectChanges, response.Changes.Count);

            Assert.NotNull(response.Changes.First().Differences);
            Assert.NotEmpty(response.Changes.First().Differences);
            Assert.Equal(expectDiffs, response.Changes.First().Differences.Count);
            Assert.True(response.Changes.First().Differences.ContainsKey(expectKeyContent));
            Assert.True(response.Changes.First().Differences.ContainsKey(expectKeySubject));
            Assert.True(response.Changes.First().Differences.ContainsKey(expectKeyStart));
            Assert.True(response.Changes.First().Differences.ContainsKey(expectKeyExpiration));
            Assert.True(response.Changes.First().Differences.ContainsKey(expectKeyStatus));
            Assert.Equal(expectContent, response.Changes.First().Differences[expectKeyContent]);
            Assert.Equal(expectSubject, response.Changes.First().Differences[expectKeySubject]);
            Assert.Equal(expectStartDate, response.Changes.First().Differences[expectKeyStart]);
            Assert.Equal(expectExpirationDate, response.Changes.First().Differences[expectKeyExpiration]);
            Assert.Equal(expectStatus.ToString(), response.Changes.First().Differences[expectKeyStatus]);
        }
    }
}
