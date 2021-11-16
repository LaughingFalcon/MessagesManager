using MessagesManager.Domain;
using MessagesManager.ExternalData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessagesManager.Repository
{
    public class AuthValidator : IAuthValidator
    {
        private readonly IUserBucket _userBucket;
        public AuthValidator(IUserBucket userBucket)
        {
            _userBucket = userBucket;
        }
        public async Task<bool> UserExistsAsync(UserModel userModel)
        {
            var key = $"{userModel.Username}:{userModel.Password}";
            var user = await _userBucket.UserRetriever(key, userModel.Token);
            return !(user is null);
        }
    }
}
