#region Libraries
using System;
using System.Data;
using IDMONEY.IO.Accounts; 
#endregion

namespace IDMONEY.IO.Databases
{
    public static class DataReaderExtensions
    {
        public static Account FormatAccount(this IDataReader reader, AccountType type = AccountType.Business)
        {
            var id = reader.FieldOrDefault<long>("account_id");

            if (id == 0)
            {
                return default(Account);
            }

            return new Account()
            {
                Id = Convert.ToInt32(reader["account_id"]),
                Type = type,
                Address = reader["address"].ToString(),
                Balance = reader.FormatBalance()
            };
        }

        public static Balance FormatBalance(this IDataReader reader)
        {
            return new Balance()
            {
                Available = Convert.ToDecimal(reader["available_balance"]),
                Blocked = Convert.ToDecimal(reader["blocked_balance"]),
            };

        }
    }
}
