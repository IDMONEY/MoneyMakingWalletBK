#region Libraries
using System.Collections.Generic;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
#endregion

namespace IDMONEY.IO.Transactions
{
    public interface ITransactionRepository
    {
        long Add(TransactionCandidate transation);
        void Update(Transaction transaction);
        Transaction Get(long? transactionId);
        IList<Transaction> GetUserTransactions(long userId);
    }
}