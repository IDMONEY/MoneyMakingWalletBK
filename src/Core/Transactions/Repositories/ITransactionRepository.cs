#region Libraries
using System.Collections.Generic;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
using IDMONEY.IO.Users;
#endregion

namespace IDMONEY.IO.Transactions
{
    public interface ITransactionRepository
    {
        long Add(TransactionCandidate transation);
        void Update(Transaction transaction);
        void Update(Transaction transaction, User user, Business business);
        Transaction Get(long? transactionId);
        IList<Transaction> GetUserTransactions(long userId);
    }
}