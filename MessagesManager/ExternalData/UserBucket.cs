using MessagesManager.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessagesManager.ExternalData
{
    public class UserBucket : IUserBucket
    {
        public async Task<UserModel> UserRetriever(string key, string token)
        {
            return await Task.FromResult(new UserModel());
        }
    }
}
