using MessagesManager.Domain;
using System.Threading.Tasks;

namespace MessagesManager.Services.BaseService
{
    public interface IManagerService
    {
        Task<ResponseModel> ProcessAsync(string authBase64Encoded, ActionModel actionModel);
    }
}