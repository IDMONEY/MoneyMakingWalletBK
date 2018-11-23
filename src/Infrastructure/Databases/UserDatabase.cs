#region Libraries
using System;
using System.Data;
using System.Threading.Tasks;
using IDMONEY.IO.Accounts;
using IDMONEY.IO.Users;
using MySql.Data.MySqlClient; 
#endregion

namespace IDMONEY.IO.Databases
{
    public class UserDatabase : RelationalDatabase
    {
        public async Task<long> InsertUser(User user)
        {
            MySqlCommand cmd = new MySqlCommand("sp_InsertUser", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_email",user.Email);
            cmd.Parameters.AddWithValue("@p_password", user.Password);
            //cmd.Parameters.AddWithValue("@p_address", user.Address);
            //cmd.Parameters.AddWithValue("@p_private_key", user.Privatekey);
            //cmd.Parameters.AddWithValue("@p_available_balance", user.AvailableBalance);
            //cmd.Parameters.AddWithValue("@p_blocked_balance", user.BlockedBalance);
            cmd.Parameters.Add(new MySqlParameter("@p_id", MySqlDbType.Int32));
            cmd.Parameters["@p_id"].Direction = ParameterDirection.Output;

            await cmd.ExecuteNonQueryAsync();

            return Convert.ToInt64(cmd.Parameters["@p_id"].Value);
        }

        public User GetUser(long userId)
        {
            return GetUser(null, userId);
        }

        public User LoginUser(string email, string password)
        {
            MySqlCommand cmd = new MySqlCommand("sp_LoginUser", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_email", email);
            cmd.Parameters.AddWithValue("@p_password", password);

            User user = null;

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = new User()
                    {
                        Email = reader["email"].ToString(),
                        Id = Convert.ToInt64(reader["user_id"])
                    };
                }
            }
        
            return user;
        }

        public User GetUser(string email)
        {
            return GetUser(email, null);
        }

        private User GetUser(string email, long? userId)
        {
            MySqlCommand cmd = new MySqlCommand("sp_GetUser", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_email", email);
            cmd.Parameters.AddWithValue("@p_user_id", userId);

            User user = null;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = new User()
                    {
                        Email = reader["email"].ToString(),
                        Privatekey = reader["private_key"].ToString(),
                        Id = Convert.ToInt64(reader["user_id"]),
                        Account = this.FormatAccount(reader)
                    };
                }
            }


            return user;
        }


        //TODO: Move this to a single file or use automapper
        private Account FormatAccount(IDataReader reader)
        {
            return new Account()
            {
                Id = Convert.ToInt32(reader["account_id"]),
                Type = AccountType.Business,
                Address = reader["address"].ToString(),
                Balance = this.FormatBalance(reader)
            };
        }

        private Balance FormatBalance(IDataReader reader)
        {
            return new Balance()
            {
                Available = Convert.ToDecimal(reader["available_balance"]),
                Blocked = Convert.ToDecimal(reader["blocked_balance"]),
            };

        }
    }
}