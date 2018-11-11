#region Libraries
using System.Collections.Generic;
using IDMONEY.IO.Transactions; 
#endregion

namespace IDMONEY.IO.Responses
{
    public class InsertBusinessResponse : Response
    {
        public Business Business { get; set; }
    }
}