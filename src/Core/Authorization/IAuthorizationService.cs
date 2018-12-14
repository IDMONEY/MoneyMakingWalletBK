#region Libraries
using System.Threading.Tasks;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses; 
#endregion

namespace IDMONEY.IO.Authorization
{
    public interface IAuthorizationService
    {
        Task<LoginUserResponse> AuthorizeAsync(LoginUserRequest request);
    }
}