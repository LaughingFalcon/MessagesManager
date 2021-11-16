using MessagesManager.Domain;
using MessagesManager.ExternalData;
using MessagesManager.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MessagesManagerTests.Repositories
{
    public class AuthValidatorTests
    {
        private readonly Mock<IUserBucket> _userBucket;
        private readonly Mock<IUserBucket> _userBucketNoUser;
        private readonly IAuthValidator _authValidator;
        private readonly IAuthValidator _authValidatorNoUser;
        public AuthValidatorTests()
        {
            _userBucket = new Mock<IUserBucket>(MockBehavior.Strict);
            _userBucket
                .Setup(x => x.UserRetriever(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new UserModel());
            _userBucketNoUser = new Mock<IUserBucket>(MockBehavior.Strict);
            _userBucketNoUser
                .Setup(x => x.UserRetriever(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(null as UserModel);

            _authValidator = new AuthValidator(_userBucket.Object);
            _authValidatorNoUser = new AuthValidator(_userBucketNoUser.Object);
        }

        [Fact]
        public async Task ValidateTest()
        {
            var userModel = new UserModel()
            {
                Username = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                Token = Guid.NewGuid().ToString()
            };

            var response = await _authValidator.UserExistsAsync(userModel);
            Assert.True(response);
        }

        [Fact]
        public async Task ValidateNoUserTest()
        {
            var userModel = new UserModel()
            {
                Username = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                Token = Guid.NewGuid().ToString()
            };

            var response = await _authValidatorNoUser.UserExistsAsync(userModel);
            Assert.False(response);
        }
    }
}
