#region Libraries
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
#endregion

namespace IDMONEY.IO.Users
{
    public interface IUserRepository
    {
        long Add(User user);
        User GetById(long id);
        User GetByCredentials(string email, string password);
        User GetByEmail(string email);
    }
}