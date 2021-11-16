using MessagesManager.Domain;
using System.Threading.Tasks;

namespace MessagesManager.ExternalData
{
    public interface IUserBucket
    {
        Task<UserModel> UserRetriever(string key, string token);
    }
}