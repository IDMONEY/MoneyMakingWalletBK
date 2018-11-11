#region Libraries
using System.Collections.Generic;
using IDMONEY.IO.Transactions; 
#endregion

namespace IDMONEY.IO.Responses
{
    public class SearchBusinessResponse : Response
    {
        public IList<Business> Businesses { get; set; }
    }
}