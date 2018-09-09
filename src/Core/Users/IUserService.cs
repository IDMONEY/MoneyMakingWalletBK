#region Libraries
using IDMONEY.IO.Cryptography;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses; 
#endregion

namespace IDMONEY.IO.Users
{
    public interface IUserService
    {
        CreateUserResponse Create(CreateUserRequest request);
    }
}
