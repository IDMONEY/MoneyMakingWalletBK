#region Libraries
using System.Collections.Generic;
using System.Threading.Tasks;
using IDMONEY.IO.Accounts;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
using IDMONEY.IO.Users;
#endregion

namespace IDMONEY.IO.Transactions
{
    public interface ITransactionRepository
    {
        Task<long> AddAsync(TransactionCandidate transation);
        Task<bool> UpdateAsync(Transaction transaction);
        Task<bool> UpdateAsync(Transaction transaction, Account fromAccount, Account toAccount);
        Task<Transaction> GetAsync(long transactionId);
        Task<IList<Transaction>> GetBusinessesTransactionsAsync(long userId);
        Task<IList<Transaction>> GetAccountTransactionsAsync(Account account);
    }
}