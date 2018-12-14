#region Libraries
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using IDMONEY.IO.Accounts;
using IDMONEY.IO.Data;
using IDMONEY.IO.Transactions;
using IDMONEY.IO.Users;
using MySql.Data.MySqlClient;
#endregion

namespace IDMONEY.IO.Databases
{
    public class TransactionDatabase : RelationalDatabaseAsync
    {
        public async Task<long> InsertTransactionAsync(TransactionCandidate candidate)
        {
            MySqlCommand cmd = new MySqlCommand("sp_InsertTransaction", Connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new MySqlParameter("@p_id", MySqlDbType.Int64));
            cmd.Parameters["@p_id"].Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@p_from_account_id", candidate.FromAccountId);
            cmd.Parameters.AddWithValue("@p_to_account_id", candidate.ToAccountId);
            cmd.Parameters.AddWithValue("@p_user_id", candidate.UserId);
            cmd.Parameters.AddWithValue("@p_amount", candidate.Amount);
            cmd.Parameters.AddWithValue("@p_registration_date", candidate.RegistrationDate);
            cmd.Parameters.AddWithValue("@p_description", candidate.Description);
            cmd.Parameters.AddWithValue("@p_status", candidate.Status);

            await cmd.ExecuteNonQueryAsync();

            return Convert.ToInt64(cmd.Parameters["@p_id"].Value);
        }

        public async Task<bool> UpdateTransactionAsync(Transaction transaction, Account fromAccount = null, Account toAccount = null)
        {
            return await this.UpdateTransactionAsync(transaction.Id, transaction.Status, transaction.ProcessingDate.Value, transaction.Amount, fromAccount, toAccount);
        }

        public async Task<bool> UpdateTransactionAsync(long? transactionId, TransactionStatus status, DateTime processingDate,
            decimal? amount = null, Account fromAccount = null, Account toAccount = null)
        {
            using (MySqlTransaction transaction = await Connection.BeginTransactionAsync())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_UpdateTransaction", Connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_id", transactionId);
                    cmd.Parameters.AddWithValue("@p_processing_date", processingDate);
                    cmd.Parameters.AddWithValue("@p_status", (int)status);

                    await cmd.ExecuteNonQueryAsync();

                    if (amount.IsNotNull() && fromAccount.IsNotNull() && toAccount.IsNotNull())
                    {
                        cmd = new MySqlCommand("sp_updateAccountBalance", Connection);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_available_balance", amount);
                        cmd.Parameters.AddWithValue("@p_blocked_balance", 0);
                        cmd.Parameters.AddWithValue("@p_tran_type", 'D');
                        cmd.Parameters.AddWithValue("@p_account_id", fromAccount.Id);

                        await cmd.ExecuteNonQueryAsync();

                        cmd = new MySqlCommand("sp_updateAccountBalance", Connection);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_available_balance", amount);
                        cmd.Parameters.AddWithValue("@p_blocked_balance", 0);
                        cmd.Parameters.AddWithValue("@p_tran_type", 'C');
                        cmd.Parameters.AddWithValue("@p_account_id", toAccount.Id);

                        await cmd.ExecuteNonQueryAsync();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return await Task.FromResult(true);
        }

        public async Task<IList<Transaction>> SearchTransactionByUserAsync(long userId)
        {

            var parameters = DataParameterBuilder.Create(this.GetFactory())
                                 .AddInParameter("@p_user_id", DbType.Int64, userId)
                                 .Parameters;

            IList<Transaction> list = new List<Transaction>();
            await this.ExecuteReaderAsync("sp_SearchTransactionByUser", CommandType.StoredProcedure, parameters, (reader) => this.MapEntities(reader, ref list, this.FormatTransaction));
            return list;
        }

        public async Task<IList<Transaction>> GetTransactionsByAccount(Account account)
        {
            var parameters = DataParameterBuilder.Create(this.GetFactory())
                                 .AddInParameter("@p_account_id", DbType.Int64, account.Id)
                                 .Parameters;

            IList<Transaction> list = new List<Transaction>();
            await this.ExecuteReaderAsync("sp_GetTransactionsByAccount", CommandType.StoredProcedure, parameters, (reader) => this.MapEntities(reader, ref list, this.FormatTransaction));
            return list;

            
        }

        public async Task<Transaction> GetTransactionAsync(long? transactionId)
        {
            var parameters = DataParameterBuilder.Create(this.GetFactory())
                                 .AddInParameter("@p_id", DbType.Int64, transactionId)
                                 .Parameters;

            Transaction transaction = default(Transaction);
            await this.ExecuteReaderAsync("sp_GetTransaction", CommandType.StoredProcedure, parameters, (reader) => this.MapEntity(reader, ref transaction, this.FormatTransaction));
            return transaction;
        }

        #region Private Methods
        private Transaction FormatTransaction(IDataReader reader)
        {
            return new Transaction()
            {
                Id = reader.FieldOrDefault<long>("id"),
                Amount = reader.FieldOrDefault<decimal>("amount"),
                Description = reader.FieldOrDefault<string>("description", string.Empty),
                ProcessingDate = reader.FieldOrDefault<DateTime>("processing_date", default(DateTime)),
                RegistrationDate = reader.FieldOrDefault<DateTime>("registration_date", default(DateTime)),
                Status = (TransactionStatus)Convert.ToInt32(reader["status"]),
                FromAccountId = reader.FieldOrDefault<long>("from_account_id"),
                ToAccountId = reader.FieldOrDefault<long>("to_account_id"),
                UserId = Convert.ToInt64(reader["user_id"])
            };
        }   
        #endregion
    }
}