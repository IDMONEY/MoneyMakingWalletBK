#region Libraries
using System.Data;
using System.Threading.Tasks;
using IDMONEY.IO.Accounts;
using IDMONEY.IO.Data; 
#endregion

namespace IDMONEY.IO.Databases
{
    public class AccountDatabase : RelationalDatabaseAsync
    {
        public virtual async Task<Account> GetAccountAsync(long accountId)
        {
            var parameters = DataParameterBuilder.Create(this.GetFactory())
                                  .AddInParameter("@p_account_id", DbType.Int64, accountId)
                                  .Parameters;

            Account business = default(Account);
            await this.ExecuteReaderAsync("sp_GetAccount", CommandType.StoredProcedure, parameters, (reader) => this.MapEntity(reader, ref business, this.FormatAccount));
            return business;
        }


        #region Private Methods
        private Account FormatAccount(IDataReader reader)
        {
            return reader.FormatAccount();
        }
        #endregion
    }
}