#region Libraries
using System.Threading.Tasks;
using IDMONEY.IO.Databases;
using IDMONEY.IO.Users;

#endregion
namespace IDMONEY.IO.Infrastructure
{
    public class MySqlNicknameRepository : INicknameRepository
    {
        public async Task<bool> Create(User user, NickName nickName)
        {
            using (var database = new NicknameDatabase())
            {
                return await database.Register(user, nickName);
            }
        }
    }
}
