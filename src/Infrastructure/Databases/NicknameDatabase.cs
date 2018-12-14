#region Libraries
using System.Threading.Tasks;
using IDMONEY.IO.Users;
using MySql.Data.MySqlClient; 
#endregion

namespace IDMONEY.IO.Databases
{
    public class NicknameDatabase : RelationalDatabase
    {
        public async Task<bool> Register(User user, NickName nickName)
        {
            MySqlCommand cmd = new MySqlCommand("sp_InsertNickname", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_user_id", user.Id);
            cmd.Parameters.AddWithValue("@p_nickname", nickName.Value);
            cmd.Parameters.AddWithValue("@p_creation_date", nickName.CreationDate);

            await cmd.ExecuteNonQueryAsync();
            return  await Task.FromResult(true);
        }
    }
}
