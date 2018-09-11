#region Libraries
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses; 
#endregion

namespace IDMONEY.IO.Authorization
{
    public interface IAuthorizationService
    {
        LoginUserResponse Authorize(LoginUserRequest request);
    }
}