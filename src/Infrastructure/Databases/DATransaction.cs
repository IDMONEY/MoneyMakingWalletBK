using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Transactions;
using IDMONEY.IO.Users;
using MySql.Data.MySqlClient;

namespace IDMONEY.IO.DataAccess
{
    public class DATransaction : DataAccess
    {
        public long InsertTransaction(int? businessId, long userId, decimal? amount, DateTime registrationDate, string description,
                int status)
        {
            MySqlCommand cmd = new MySqlCommand("sp_InsertTransaction", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new MySqlParameter("@p_id", MySqlDbType.Int32));
            cmd.Parameters["@p_id"].Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@p_business_id", businessId);
            cmd.Parameters.AddWithValue("@p_user_id", userId);
            cmd.Parameters.AddWithValue("@p_amount", amount);
            cmd.Parameters.AddWithValue("@p_registration_date", registrationDate);
            cmd.Parameters.AddWithValue("@p_description", description);
            cmd.Parameters.AddWithValue("@p_status", status);

            cmd.ExecuteNonQuery();

            return Convert.ToInt64(cmd.Parameters["@p_id"].Value);
        }

        public void UpdateTransaction(long? transactionId, int status, DateTime processingDate,
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
                    cmd.Parameters.AddWithValue("@p_status", status);

                    cmd.ExecuteNonQuery();

                    if (amount != null)
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
                    throw ex;
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
                        Status = Convert.ToInt32(reader["status"]),
                        StatusName = reader["StatusName"].ToString(),
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
                        Status = Convert.ToInt32(reader["status"]),
                        StatusName = reader["StatusName"].ToString(),
                        UserId = (int?)Convert.ToInt64(reader["user_id"])
                    };
                }
            }

            return transaction;
        }
    }
}
