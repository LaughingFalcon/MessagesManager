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
    public class ToUpdateMessageTests
    {
        private readonly IToUpdateMessage _toUpdateMessage;
        public ToUpdateMessageTests()
        {
            _toUpdateMessage = new ToUpdateMessage();
        }

        [Fact]
        public void UpdateTest()
        {
            var baseVersion = 1;
            var id = Guid.NewGuid().ToString();
            var username = Guid.NewGuid().ToString();
            #region EXPECTS
            var expectVersion = 2;
            var expectContent = Guid.NewGuid().ToString();
            var expectExpirationDate = Guid.NewGuid().ToString();
            var expectStartDate = Guid.NewGuid().ToString();
            var expectSubject = Guid.NewGuid().ToString();
            var expectStatus = MessageStatusEnum.MODIFIED;
            var expectChangeDiff = 5;
            var expectKeySubject = "Subject";
            var expectKeyContent = "Content";
            var expectKeyStartDate = "StartDate";
            var expectKeyExpirationDate = "ExpirationDate";
            var expectKeyStatus = "Status";
            #endregion

            var oldMessage = new MessageModel()
            {
                Id = id,
                Content = Guid.NewGuid().ToString(),
                Subject = Guid.NewGuid().ToString(),
                Version = baseVersion,
                Creator = username,
                ExpirationDate = Guid.NewGuid().ToString(),
                StartDate = Guid.NewGuid().ToString(),
                Status = MessageStatusEnum.NEW,
                Changes = new List<Changes>()
                {
                    new Changes()
                }
            };
            var newMessage = new MessageModel()
            {
                Id = id,
                Content = expectContent,
                ExpirationDate = expectExpirationDate,
                StartDate = expectStartDate,
                Subject = expectSubject
            };

            var response = _toUpdateMessage.SetChanges(oldMessage, newMessage);
            Assert.NotNull(response);

            Assert.Equal(id, response.Id);
            Assert.Equal(expectVersion, response.Version);

            Assert.Equal(oldMessage.Creator, response.Creator);
            Assert.NotEqual(oldMessage.Content, response.Content);
            Assert.NotEqual(oldMessage.Subject, response.Subject);
            Assert.NotEqual(oldMessage.ExpirationDate, response.ExpirationDate);
            Assert.NotEqual(oldMessage.StartDate, response.StartDate);
            Assert.Equal(expectContent, response.Content);
            Assert.Equal(expectExpirationDate, response.ExpirationDate);
            Assert.Equal(expectStartDate, response.StartDate);
            Assert.Equal(expectSubject, response.Subject);
            Assert.Equal(expectStatus, response.Status);

            Assert.NotNull(response.Changes);
            Assert.NotEmpty(response.Changes);
            Assert.NotNull(response.Changes.Last().Differences);
            Assert.NotEmpty(response.Changes.Last().Differences);
            Assert.Equal(expectChangeDiff, response.Changes.Last().Differences.Count);

            Assert.True(response.Changes.Last().Differences.ContainsKey(expectKeyContent));
            Assert.True(response.Changes.Last().Differences.ContainsKey(expectKeyExpirationDate));
            Assert.True(response.Changes.Last().Differences.ContainsKey(expectKeyStartDate));
            Assert.True(response.Changes.Last().Differences.ContainsKey(expectKeyStatus));
            Assert.True(response.Changes.Last().Differences.ContainsKey(expectKeySubject));

            Assert.Equal(expectContent, response.Changes.Last().Differences[expectKeyContent]);
            Assert.Equal(expectExpirationDate, response.Changes.Last().Differences[expectKeyExpirationDate]);
            Assert.Equal(expectStartDate, response.Changes.Last().Differences[expectKeyStartDate]);
            Assert.Equal(expectSubject, response.Changes.Last().Differences[expectKeySubject]);
            Assert.Equal(expectStatus.ToString(), response.Changes.Last().Differences[expectKeyStatus]);
        }

        [Fact]
        public void UpdateSubjectTest()
        {
            var baseVersion = 1;
            var id = Guid.NewGuid().ToString();
            var username = Guid.NewGuid().ToString();
            #region EXPECTS
            var expectVersion = 2;
            var expectContent = Guid.NewGuid().ToString();
            var expectExpirationDate = Guid.NewGuid().ToString();
            var expectStartDate = Guid.NewGuid().ToString();
            var expectSubject = Guid.NewGuid().ToString();
            var expectStatus = MessageStatusEnum.MODIFIED;
            var expectChangeDiff = 1;
            var expectKeySubject = "Subject";
            var notExpectKeyContent = "Content";
            var notExpectKeyStartDate = "StartDate";
            var notExpectKeyExpirationDate = "ExpirationDate";
            var notExpectKeyStatus = "Status";
            #endregion

            var oldMessage = new MessageModel()
            {
                Id = id,
                Content = expectContent,
                Subject = Guid.NewGuid().ToString(),
                Version = baseVersion,
                Creator = username,
                ExpirationDate = expectExpirationDate,
                StartDate = expectStartDate,
                Status = expectStatus,
                Changes = new List<Changes>()
                {
                    new Changes()
                }
            };
            var newMessage = new MessageModel()
            {
                Id = id,
                Content = expectContent,
                ExpirationDate = expectExpirationDate,
                StartDate = expectStartDate,
                Subject = expectSubject
            };

            var response = _toUpdateMessage.SetChanges(oldMessage, newMessage);
            Assert.NotNull(response);

            Assert.Equal(id, response.Id);
            Assert.Equal(expectVersion, response.Version);

            Assert.Equal(oldMessage.Creator, response.Creator);
            Assert.Equal(oldMessage.Content, response.Content);
            Assert.NotEqual(oldMessage.Subject, response.Subject);
            Assert.Equal(oldMessage.ExpirationDate, response.ExpirationDate);
            Assert.Equal(oldMessage.StartDate, response.StartDate);

            Assert.Equal(expectContent, response.Content);
            Assert.Equal(expectExpirationDate, response.ExpirationDate);
            Assert.Equal(expectStartDate, response.StartDate);
            Assert.Equal(expectSubject, response.Subject);
            Assert.Equal(expectStatus, response.Status);

            Assert.NotNull(response.Changes);
            Assert.NotEmpty(response.Changes);
            Assert.NotNull(response.Changes.Last().Differences);
            Assert.NotEmpty(response.Changes.Last().Differences);
            Assert.Equal(expectChangeDiff, response.Changes.Last().Differences.Count);

            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeyContent));
            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeyExpirationDate));
            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeyStartDate));
            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeyStatus));
            Assert.True(response.Changes.Last().Differences.ContainsKey(expectKeySubject));

            Assert.Equal(expectSubject, response.Changes.Last().Differences[expectKeySubject]);
        }

        [Fact]
        public void UpdateContentTest()
        {
            var baseVersion = 1;
            var id = Guid.NewGuid().ToString();
            var username = Guid.NewGuid().ToString();
            #region EXPECTS
            var expectVersion = 2;
            var expectContent = Guid.NewGuid().ToString();
            var expectExpirationDate = Guid.NewGuid().ToString();
            var expectStartDate = Guid.NewGuid().ToString();
            var expectSubject = Guid.NewGuid().ToString();
            var expectStatus = MessageStatusEnum.MODIFIED;
            var expectChangeDiff = 1;
            var notExpectKeySubject = "Subject";
            var expectKeyContent = "Content";
            var notExpectKeyStartDate = "StartDate";
            var notExpectKeyExpirationDate = "ExpirationDate";
            var notExpectKeyStatus = "Status";
            #endregion

            var oldMessage = new MessageModel()
            {
                Id = id,
                Content = Guid.NewGuid().ToString(),
                Subject = expectSubject,
                Version = baseVersion,
                Creator = username,
                ExpirationDate = expectExpirationDate,
                StartDate = expectStartDate,
                Status = expectStatus,
                Changes = new List<Changes>()
                {
                    new Changes()
                }
            };
            var newMessage = new MessageModel()
            {
                Id = id,
                Content = expectContent,
                ExpirationDate = expectExpirationDate,
                StartDate = expectStartDate,
                Subject = expectSubject
            };

            var response = _toUpdateMessage.SetChanges(oldMessage, newMessage);
            Assert.NotNull(response);

            Assert.Equal(id, response.Id);
            Assert.Equal(expectVersion, response.Version);

            Assert.Equal(oldMessage.Creator, response.Creator);
            Assert.NotEqual(oldMessage.Content, response.Content);
            Assert.Equal(oldMessage.Subject, response.Subject);
            Assert.Equal(oldMessage.ExpirationDate, response.ExpirationDate);
            Assert.Equal(oldMessage.StartDate, response.StartDate);

            Assert.Equal(expectContent, response.Content);
            Assert.Equal(expectExpirationDate, response.ExpirationDate);
            Assert.Equal(expectStartDate, response.StartDate);
            Assert.Equal(expectSubject, response.Subject);
            Assert.Equal(expectStatus, response.Status);

            Assert.NotNull(response.Changes);
            Assert.NotEmpty(response.Changes);
            Assert.NotNull(response.Changes.Last().Differences);
            Assert.NotEmpty(response.Changes.Last().Differences);
            Assert.Equal(expectChangeDiff, response.Changes.Last().Differences.Count);

            Assert.True(response.Changes.Last().Differences.ContainsKey(expectKeyContent));
            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeyExpirationDate));
            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeyStartDate));
            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeyStatus));
            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeySubject));

            Assert.Equal(expectContent, response.Changes.Last().Differences[expectKeyContent]);
        }

        [Fact]
        public void UpdateExpirationDateTest()
        {
            var baseVersion = 1;
            var id = Guid.NewGuid().ToString();
            var username = Guid.NewGuid().ToString();
            #region EXPECTS
            var expectVersion = 2;
            var expectContent = Guid.NewGuid().ToString();
            var expectExpirationDate = Guid.NewGuid().ToString();
            var expectStartDate = Guid.NewGuid().ToString();
            var expectSubject = Guid.NewGuid().ToString();
            var expectStatus = MessageStatusEnum.MODIFIED;
            var expectChangeDiff = 1;
            var notExpectKeySubject = "Subject";
            var notExpectKeyContent = "Content";
            var notExpectKeyStartDate = "StartDate";
            var expectKeyExpirationDate = "ExpirationDate";
            var notExpectKeyStatus = "Status";
            #endregion

            var oldMessage = new MessageModel()
            {
                Id = id,
                Content = expectContent,
                Subject = expectSubject,
                Version = baseVersion,
                Creator = username,
                ExpirationDate = Guid.NewGuid().ToString(),
                StartDate = expectStartDate,
                Status = expectStatus,
                Changes = new List<Changes>()
                {
                    new Changes()
                }
            };
            var newMessage = new MessageModel()
            {
                Id = id,
                Content = expectContent,
                ExpirationDate = expectExpirationDate,
                StartDate = expectStartDate,
                Subject = expectSubject
            };

            var response = _toUpdateMessage.SetChanges(oldMessage, newMessage);
            Assert.NotNull(response);

            Assert.Equal(id, response.Id);
            Assert.Equal(expectVersion, response.Version);

            Assert.Equal(oldMessage.Creator, response.Creator);
            Assert.Equal(oldMessage.Content, response.Content);
            Assert.Equal(oldMessage.Subject, response.Subject);
            Assert.NotEqual(oldMessage.ExpirationDate, response.ExpirationDate);
            Assert.Equal(oldMessage.StartDate, response.StartDate);

            Assert.Equal(expectContent, response.Content);
            Assert.Equal(expectExpirationDate, response.ExpirationDate);
            Assert.Equal(expectStartDate, response.StartDate);
            Assert.Equal(expectSubject, response.Subject);
            Assert.Equal(expectStatus, response.Status);

            Assert.NotNull(response.Changes);
            Assert.NotEmpty(response.Changes);
            Assert.NotNull(response.Changes.Last().Differences);
            Assert.NotEmpty(response.Changes.Last().Differences);
            Assert.Equal(expectChangeDiff, response.Changes.Last().Differences.Count);

            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeyContent));
            Assert.True(response.Changes.Last().Differences.ContainsKey(expectKeyExpirationDate));
            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeyStartDate));
            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeyStatus));
            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeySubject));

            Assert.Equal(expectExpirationDate, response.Changes.Last().Differences[expectKeyExpirationDate]);
        }

        [Fact]
        public void UpdateStartDateTest()
        {
            var baseVersion = 1;
            var id = Guid.NewGuid().ToString();
            var username = Guid.NewGuid().ToString();
            #region EXPECTS
            var expectVersion = 2;
            var expectContent = Guid.NewGuid().ToString();
            var expectExpirationDate = Guid.NewGuid().ToString();
            var expectStartDate = Guid.NewGuid().ToString();
            var expectSubject = Guid.NewGuid().ToString();
            var expectStatus = MessageStatusEnum.MODIFIED;
            var expectChangeDiff = 1;
            var notExpectKeySubject = "Subject";
            var notExpectKeyContent = "Content";
            var expectKeyStartDate = "StartDate";
            var notExpectKeyExpirationDate = "ExpirationDate";
            var notExpectKeyStatus = "Status";
            #endregion

            var oldMessage = new MessageModel()
            {
                Id = id,
                Content = expectContent,
                Subject = expectSubject,
                Version = baseVersion,
                Creator = username,
                ExpirationDate = expectExpirationDate,
                StartDate = Guid.NewGuid().ToString(),
                Status = expectStatus,
                Changes = new List<Changes>()
                {
                    new Changes()
                }
            };
            var newMessage = new MessageModel()
            {
                Id = id,
                Content = expectContent,
                ExpirationDate = expectExpirationDate,
                StartDate = expectStartDate,
                Subject = expectSubject
            };

            var response = _toUpdateMessage.SetChanges(oldMessage, newMessage);
            Assert.NotNull(response);

            Assert.Equal(id, response.Id);
            Assert.Equal(expectVersion, response.Version);

            Assert.Equal(oldMessage.Creator, response.Creator);
            Assert.Equal(oldMessage.Content, response.Content);
            Assert.Equal(oldMessage.Subject, response.Subject);
            Assert.Equal(oldMessage.ExpirationDate, response.ExpirationDate);
            Assert.NotEqual(oldMessage.StartDate, response.StartDate);

            Assert.Equal(expectContent, response.Content);
            Assert.Equal(expectExpirationDate, response.ExpirationDate);
            Assert.Equal(expectStartDate, response.StartDate);
            Assert.Equal(expectSubject, response.Subject);
            Assert.Equal(expectStatus, response.Status);

            Assert.NotNull(response.Changes);
            Assert.NotEmpty(response.Changes);
            Assert.NotNull(response.Changes.Last().Differences);
            Assert.NotEmpty(response.Changes.Last().Differences);
            Assert.Equal(expectChangeDiff, response.Changes.Last().Differences.Count);

            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeyContent));
            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeyExpirationDate));
            Assert.True(response.Changes.Last().Differences.ContainsKey(expectKeyStartDate));
            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeyStatus));
            Assert.False(response.Changes.Last().Differences.ContainsKey(notExpectKeySubject));

            Assert.Equal(expectStartDate, response.Changes.Last().Differences[expectKeyStartDate]);
        }

        [Fact]
        public void NoUpdateTest()
        {
            var baseVersion = 1;
            var id = Guid.NewGuid().ToString();
            var username = Guid.NewGuid().ToString();
            var content = Guid.NewGuid().ToString();
            var subject = Guid.NewGuid().ToString();
            var expirationDays = Guid.NewGuid().ToString();
            var startDate = Guid.NewGuid().ToString();
            
            var oldMessage = new MessageModel()
            {
                Id = id,
                Content = content,
                Subject = subject,
                Version = baseVersion,
                Creator = username,
                ExpirationDate = expirationDays,
                StartDate = startDate,
                Status = MessageStatusEnum.MODIFIED,
                Changes = new List<Changes>()
                {
                    new Changes()
                }
            };
            var newMessage = new MessageModel()
            {
                Id = id,
                Content = content,
                ExpirationDate = expirationDays,
                StartDate = startDate,
                Subject = subject
            };

            var response = _toUpdateMessage.SetChanges(oldMessage, newMessage);
            Assert.Null(response);
        }
    }
}
