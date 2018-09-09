#region Libraries
using IDMONEY.IO.Transactions; 
#endregion

namespace IDMONEY.IO.Requests
{
    public class InsertTransactionRequest : Request
    {
        public Transaction Transaction { get; set; }
    }
}