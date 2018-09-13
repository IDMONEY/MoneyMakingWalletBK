#region Libraries
using System.Collections.Generic;
using IDMONEY.IO.DataAccess;
using IDMONEY.IO.Transactions;
#endregion

namespace IDMONEY.IO.Infrastructure
{
    public class MySqlTransactionRepository : ITransactionRepository
    {
        public long Add(TransactionCandidate transation)
        {
            using (var database = new DATransaction())
            {
                return database.InsertTransaction(transation);
            }
        }

        public Transaction Get(long? transactionId)
        {
            using (var database = new DATransaction())
            {
                return database.GetTransaction(transactionId);
            }
        }

        public IList<Transaction> GetUserTransactions(long userId)
        {
            using (var database = new DATransaction())
            {
                return database.SearchTransactionByUser(userId);
            }
        }

        public void Update(Transaction transaction)
        {
            using (var database = new DATransaction())
            {
                database.UpdateTransaction(transaction);
            }
        }
    }
}
