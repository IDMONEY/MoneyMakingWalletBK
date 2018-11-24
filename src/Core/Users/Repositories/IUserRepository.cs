#region Libraries
using System.Threading.Tasks;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
#endregion

namespace IDMONEY.IO.Users
{
    public interface IUserRepository
    {
        Task<long> AddAsync(User user);
        Task<User> GetByIdAsync(long id);
        Task<User> GetByCredentialsAsync(string email, string password);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByNicknameAsync(string nickname);
    }
}