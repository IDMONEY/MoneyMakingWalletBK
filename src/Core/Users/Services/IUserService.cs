#region Libraries
using System.Security.Claims;
using System.Threading.Tasks;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses; 
#endregion

namespace IDMONEY.IO.Users
{
    public interface IUserService
    {
        Task<CreateUserResponse> Create(CreateUserRequest request);
        Task<UserResponse> GetUser(ClaimsPrincipal claimsPrincipal);
    }
}