#region Libraries
using IDMONEY.IO.Users;
#endregion

namespace IDMONEY.IO.Responses
{
    public class UserResponse : Response
    {
        public User User { get; set; }
    }
}