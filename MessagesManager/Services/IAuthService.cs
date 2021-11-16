using MessagesManager.Domain;
using System.Threading.Tasks;

namespace MessagesManager.Services
{
    public interface IAuthService
    {
        Task<bool> ValidadeUserAsync(UserModel userModel);
    }
}