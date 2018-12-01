#region Libraries
using System;
using System.Data;
using System.Threading.Tasks;
using IDMONEY.IO.Accounts;
using IDMONEY.IO.Data;
using IDMONEY.IO.Users;
using MySql.Data.MySqlClient; 
#endregion

namespace IDMONEY.IO.Databases
{
    public class UserDatabase : RelationalDatabaseAsync
    {
        public async Task<long> InsertUserAsync(User user)
        {
            MySqlCommand cmd = new MySqlCommand("sp_InsertUser", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_email",user.Email);
            cmd.Parameters.AddWithValue("@p_password", user.Password);
            //cmd.Parameters.AddWithValue("@p_address", user.Address);
            //cmd.Parameters.AddWithValue("@p_private_key", user.Privatekey);
            //cmd.Parameters.AddWithValue("@p_available_balance", user.AvailableBalance);
            //cmd.Parameters.AddWithValue("@p_blocked_balance", user.BlockedBalance);
            cmd.Parameters.Add(new MySqlParameter("@p_id", MySqlDbType.Int64));
            cmd.Parameters["@p_id"].Direction = ParameterDirection.Output;

            await cmd.ExecuteNonQueryAsync();

            return Convert.ToInt64(cmd.Parameters["@p_id"].Value);
        }

        public async Task<User> GetUserAsync(long userId)
        {
            return await GetUserAsync(null, userId);
        }

        public async Task<User> GetByNicknameAsync(string nickname)
        {
            var parameters = DataParameterBuilder.Create(this.GetFactory())
                      .AddInParameter("@p_nickname", DbType.String, nickname)
                      .Parameters;

            User user = default(User);
            await this.ExecuteReaderAsync("sp_GetUserByNickname", CommandType.StoredProcedure, parameters, (reader) => this.MapEntity(reader, ref user, this.FormatUser));
            return user;
        }

        public async Task<User> LoginUserAsync(string email, string password)
        {
            var parameters = DataParameterBuilder.Create(this.GetFactory())
                                 .AddInParameter("@p_email", DbType.String, email)
                                 .AddInParameter("@p_password", DbType.String, password)
                                 .Parameters;

            User user = default(User);
            await this.ExecuteReaderAsync("sp_LoginUser", CommandType.StoredProcedure, parameters, (reader) => this.MapEntity(reader, ref user, this.FormatUser));
            return user;
        }

        public  async Task<User> GetUserAsync(string email)
        {
            return await GetUserAsync(email, null);
        }

        private async Task<User> GetUserAsync(string email, long? userId)
        {
            var parameters = DataParameterBuilder.Create(this.GetFactory())
                                  .AddInParameter("@p_email", DbType.String, email)
                                  .AddInParameter("@p_user_id", DbType.Int64, userId)
                                  .Parameters;

            User user = default(User);
            await this.ExecuteReaderAsync("sp_GetUser", CommandType.StoredProcedure, parameters, (reader) => this.MapEntity(reader, ref user, this.FormatUser));
            return user;
        }

        private User FormatUser(IDataReader reader)
        {
            return new User()
            {
                Email = reader.FieldOrDefault<string>("email"),
                Id = reader.FieldOrDefault<long>("user_id"),
                Account = reader.FormatAccount(AccountType.Personal)
            };
        }
    }
}