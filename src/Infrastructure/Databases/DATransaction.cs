#region Libraries
using System;
using System.Collections.Generic;
using System.Data;
using IDMONEY.IO.Transactions;
using IDMONEY.IO.Users;
using MySql.Data.MySqlClient; 
#endregion

namespace IDMONEY.IO.DataAccess
{
    public class DATransaction : DataAccess
    {
        public long InsertTransaction(TransactionCandidate candidate)
        {
            MySqlCommand cmd = new MySqlCommand("sp_InsertTransaction", Connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new MySqlParameter("@p_id", MySqlDbType.Int32));
            cmd.Parameters["@p_id"].Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@p_business_id", candidate.BusinessId);
            cmd.Parameters.AddWithValue("@p_user_id", candidate.UserId);
            cmd.Parameters.AddWithValue("@p_amount", candidate.Amount);
            cmd.Parameters.AddWithValue("@p_registration_date", candidate.RegistrationDate);
            cmd.Parameters.AddWithValue("@p_description", candidate.Description);
            cmd.Parameters.AddWithValue("@p_status", candidate.Status);

            cmd.ExecuteNonQuery();

            return Convert.ToInt64(cmd.Parameters["@p_id"].Value);
        }

        public void UpdateTransaction(Transaction transaction, Business business = null, User user = null)
        {
            this.UpdateTransaction(transaction.Id, transaction.Status, transaction.ProcessingDate.Value, transaction.Amount, business, user);
        }

        public void UpdateTransaction(long? transactionId, TransactionStatus status, DateTime processingDate,
            decimal? amount = null, Business business = null, User user = null)
        {
            using (MySqlTransaction transaction = Connection.BeginTransaction())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_UpdateTransaction", Connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_id", transactionId);
                    cmd.Parameters.AddWithValue("@p_processing_date", processingDate);
                    cmd.Parameters.AddWithValue("@p_status", (int)status);

                    cmd.ExecuteNonQuery();

                    if (amount.IsNotNull())
                    {
                        cmd = new MySqlCommand("sp_UpdateBalanceUser", Connection);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_available_balance", amount);
                        cmd.Parameters.AddWithValue("@p_blocked_balance", 0);
                        cmd.Parameters.AddWithValue("@p_isSubtraction", true);
                        cmd.Parameters.AddWithValue("@p_isSum", false);
                        cmd.Parameters.AddWithValue("@p_userId", user.Id);

                        cmd.ExecuteNonQuery();

                        cmd = new MySqlCommand("sp_UpdateBalanceBusiness", Connection);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_blocked_balance", 0);
                        cmd.Parameters.AddWithValue("@p_available_balance", amount);
                        cmd.Parameters.AddWithValue("@p_isSubtraction", false); 
                        cmd.Parameters.AddWithValue("@p_isSum", true);
                        cmd.Parameters.AddWithValue("@p_businessId", business.Id);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<Transaction> SearchTransactionByUser(long userId)
        {
            MySqlCommand cmd = new MySqlCommand("sp_SearchTransactionByUser", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_user_id", userId);

            List<Transaction> list = new List<Transaction>();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Transaction()
                    {
                        Id = Convert.ToInt64(reader["id"]),
                        Amount = Convert.ToDecimal(reader["amount"]),
                        BusinessId = Convert.ToInt32(reader["business_id"]),
                        BusinessName = reader["name"].ToString(),
                        Image = reader["image"].ToString(),
                        Description = Convert.IsDBNull(reader["description"]) ? null : reader["description"].ToString(),
                        ProcessingDate = Convert.IsDBNull(reader["processing_date"]) ? null : (DateTime?)Convert.ToDateTime(reader["processing_date"]),
                        RegistrationDate = Convert.ToDateTime(reader["registration_date"]),
                        Status = (TransactionStatus)Convert.ToInt32(reader["status"]),
                        //StatusName = reader["StatusName"].ToString(),
                        UserId = (int?)Convert.ToInt64(reader["user_id"])
                    });
                }
            }

            return list;
        }

        public Transaction GetTransaction(long? transactionId)
        {
            MySqlCommand cmd = new MySqlCommand("sp_GetTransaction", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_id", transactionId);

            Transaction transaction = null;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    transaction = new Transaction()
                    {
                        Id = Convert.ToInt64(reader["id"]),
                        Amount = Convert.ToDecimal(reader["amount"]),
                        BusinessId = Convert.ToInt32(reader["business_id"]),
                        BusinessName = reader["name"].ToString(),
                        Image = reader["image"].ToString(),
                        Description = Convert.IsDBNull(reader["description"]) ? null : reader["description"].ToString(),
                        ProcessingDate = Convert.IsDBNull(reader["processing_date"]) ? null : (DateTime?)Convert.ToDateTime(reader["processing_date"]),
                        RegistrationDate = Convert.ToDateTime(reader["registration_date"]),
                        Status = (TransactionStatus)Convert.ToInt32(reader["status"]),
                        //StatusName = reader["StatusName"].ToString(),
                        UserId = (int?)Convert.ToInt64(reader["user_id"])
                    };
                }
            }

            return transaction;
        }
    }
}
