using MessagesManager.Domain;
using MessagesManager.Factories;
using MessagesManager.Infrastructure.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MessagesManager.Services.BaseService
{
    public class ManagerService : IManagerService
    {
        private readonly IAuthService _authService;
        private readonly IMessageFactory _messageFactory;
        public ManagerService(IAuthService authService, IMessageFactory messageFactory)
        {
            _authService = authService;
            _messageFactory = messageFactory;
        }
        public async Task<ResponseModel> ProcessAsync(string authBase64Encoded, ActionModel actionModel)
        {
            var userAuth = JsonConvert.DeserializeObject<UserModel>(authBase64Encoded.FromBase64());
            var validUser = await _authService.ValidadeUserAsync(userAuth);

            var messageService = _messageFactory.Create(actionModel.Action);
            var response = validUser ? await messageService.ExecuteAsync(actionModel, userAuth.Username) : null;

            return response;
        }
    }
}
