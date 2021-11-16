using MessagesManager.Domain;
using MessagesManager.ExternalData;
using MessagesManager.Repository;
using MessagesManager.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MessagesManagerTests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IAuthValidator> _authValidator;
        private readonly IAuthService _authService;
        public AuthServiceTests()
        {
            _authValidator = new Mock<IAuthValidator>(MockBehavior.Strict);
            _authValidator
                .Setup(x => x.UserExistsAsync(It.Is<UserModel>(u => !string.IsNullOrEmpty(u.Username) && !string.IsNullOrEmpty(u.Password) && !string.IsNullOrEmpty(u.Token))))
                .ReturnsAsync(true);
            _authService = new AuthService(_authValidator.Object);
        }

        [Fact]
        public async Task TestCredentialsAsync()
        {
            var userModel = new UserModel()
            {
                Password = Guid.NewGuid().ToString(),
                Username = Guid.NewGuid().ToString(),
                Token = Guid.NewGuid().ToString()
            };

            var response = await _authService.ValidadeUserAsync(userModel);
            Assert.True(response);
        }

        [Fact]
        public async Task TestCredentialsNoPasswordAsync()
        {
            var userModel = new UserModel()
            {
                Password = string.Empty,
                Username = Guid.NewGuid().ToString(),
                Token = Guid.NewGuid().ToString()
            };

            var response = await _authService.ValidadeUserAsync(userModel);
            Assert.False(response);
        }

        [Fact]
        public async Task TestCredentialsNoUsernameAsync()
        {
            var userModel = new UserModel()
            {
                Password = Guid.NewGuid().ToString(),
                Username = string.Empty,
                Token = Guid.NewGuid().ToString()
            };

            var response = await _authService.ValidadeUserAsync(userModel);
            Assert.False(response);
        }

        [Fact]
        public async Task TestCredentialsNoTokenAsync()
        {
            var userModel = new UserModel()
            {
                Password = Guid.NewGuid().ToString(),
                Username = Guid.NewGuid().ToString(),
                Token = string.Empty
            };

            var response = await _authService.ValidadeUserAsync(userModel);
            Assert.False(response);
        }
    }
}
