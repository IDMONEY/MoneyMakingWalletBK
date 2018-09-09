#region Libraries
using IDMONEY.IO.Users; 
#endregion

namespace IDMONEY.IO.Responses
{
    public class LoginUserResponse : Response
    {
        public User User { get; set; }

        public string Token { get; set; }
    }
}