#region Libraries
using System.Threading.Tasks;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
#endregion

namespace IDMONEY.IO.Users
{
    public interface IUserRepository
    {
        Task<long> Add(User user);
        User GetById(long id);
        User GetByCredentials(string email, string password);
        User GetByEmail(string email);
    }
}