#region Libraries
using System.Security.Claims;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses; 
#endregion

namespace IDMONEY.IO.Users
{
    public interface IUserService
    {
        CreateUserResponse Create(CreateUserRequest request);
        UserResponse GetUser(ClaimsPrincipal claimsPrincipal);
    }
}
