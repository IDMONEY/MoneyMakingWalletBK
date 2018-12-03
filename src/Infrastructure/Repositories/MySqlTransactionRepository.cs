#region Libraries
using System.Collections.Generic;
using System.Threading.Tasks;
using IDMONEY.IO.Accounts;
using IDMONEY.IO.Databases;
using IDMONEY.IO.Transactions;
using IDMONEY.IO.Users;
#endregion

namespace IDMONEY.IO.Infrastructure
{
    public class MySqlTransactionRepository : ITransactionRepository
    {
        public async Task<long> AddAsync(TransactionCandidate transation)
        {
            using (var database = new TransactionDatabase())
            {
                return await database.InsertTransactionAsync(transation);
            }
        }

        public async Task<IList<Transaction>> GetAccountTransactionsAsync(Account account)
        {
            using (var database = new TransactionDatabase())
            {
                return await database.GetTransactionsByAccount(account);
            }
        }

        public async Task<Transaction> GetAsync(long? transactionId)
        {
            using (var database = new TransactionDatabase())
            {
                return await database.GetTransactionAsync(transactionId);
            }
        }

        public async Task<IList<Transaction>> GetUserTransactionsAsync(long userId)
        {
            using (var database = new TransactionDatabase())
            {
                return await database.SearchTransactionByUserAsync(userId);
            }
        }

        public async Task<bool> UpdateAsync(Transaction transaction)
        {
            using (var database = new TransactionDatabase())
            {
                return await database.UpdateTransactionAsync(transaction);
            }
        }

        public async Task<bool> UpdateAsync(Transaction transaction, Account fromAccount, Account toAccount)
        {
            using (var database = new TransactionDatabase())
            {
                return await database.UpdateTransactionAsync(transaction, fromAccount, toAccount);
            }
        }
    }
}
