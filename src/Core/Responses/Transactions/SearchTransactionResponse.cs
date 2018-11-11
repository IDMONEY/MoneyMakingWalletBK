#region Libraries
using System.Collections.Generic;
using IDMONEY.IO.Transactions;

#endregion
namespace IDMONEY.IO.Responses
{
    public class SearchTransactionResponse : Response
    {
        public IList<Transaction> Transactions { get; set; }
    }
}
