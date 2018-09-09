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
        User GetByEmail(string email);
    }
}