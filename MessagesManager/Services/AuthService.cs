using MessagesManager.Domain;
using MessagesManager.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessagesManager.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthValidator _authValidator;
        public AuthService(IAuthValidator authValidator)
        {
            _authValidator = authValidator;
        }
        public async Task<bool> ValidadeUserAsync(UserModel userModel)
        {
            var userIsValid = false;
            try
            {
                userIsValid = await _authValidator.UserExistsAsync(userModel);
                Console.WriteLine($"Credential valid: {userIsValid}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while trying to validate user {userModel.Username}");
                return userIsValid;
            }
            return userIsValid;
        }
    }
}
