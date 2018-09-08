#region MyRegion
using System.Collections.Generic;
using IDMONEY.IO.Transactions; 
#endregion

namespace IDMONEY.IO.Responses
{
    public class SearchBusinessResponse : Response
    {
        public List<Business> Businesses { get; set; }
    }
}