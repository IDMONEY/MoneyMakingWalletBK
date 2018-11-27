#region Libraries
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IDMONEY.IO.Accounts;
using IDMONEY.IO.Databases;
using IDMONEY.IO.Transactions; 
#endregion

namespace IDMONEY.IO.Infrastructure
{
    public class MySqlAccountRepository : IAccountRepository
    {
        public async Task<Account> GetAsync(long id)
        {
            using (var database = new AccountDatabase())
            {
                return await database.GetAccountAsync(id);
            }
        }
    }
}