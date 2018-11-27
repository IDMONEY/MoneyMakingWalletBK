#region Libraries
using System.Security.Claims;
using System.Threading.Tasks;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses; 
#endregion

namespace IDMONEY.IO.Transactions
{
    public interface ITransactionService
    {
        Task<InsertTransactionResponse> AddAsync(InsertTransactionRequest request, ClaimsPrincipal claimsPrincipal);
        Task<SearchTransactionResponse> GetUserTransactionsAsync(ClaimsPrincipal claimsPrincipal);
    }
}