using MessagesManager.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessagesManager.Services.Implementations
{
    public interface IMessageService
    {
        public Task<ResponseModel> ExecuteAsync(ActionModel actionModel, string username);
    }
}
