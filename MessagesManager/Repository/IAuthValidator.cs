using MessagesManager.Domain;
using System.Threading.Tasks;

namespace MessagesManager.Repository
{
    public interface IAuthValidator
    {
        Task<bool> UserExistsAsync(UserModel userModel);
    }
}