#region Libraries
using System.Security.Claims;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses; 
#endregion

namespace IDMONEY.IO.Transactions
{
    public interface ITransactionService
    {
        InsertTransactionResponse Add(InsertTransactionRequest request);
        SearchTransactionResponse GetUserTransactions(ClaimsPrincipal claimsPrincipal);
    }
}
