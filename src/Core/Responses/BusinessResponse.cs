#region Libraries
using IDMONEY.IO.Transactions; 
#endregion

namespace IDMONEY.IO.Responses
{
    public class BusinessResponse : Response
    {
        public Business Business { get; set; }
    }
}