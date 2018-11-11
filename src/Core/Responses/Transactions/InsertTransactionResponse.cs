#region Libraries
using IDMONEY.IO.Transactions;
#endregion

namespace IDMONEY.IO.Responses
{
    public class InsertTransactionResponse : Response
    {
        public Transaction Transaction { get; set; }
    }
}
