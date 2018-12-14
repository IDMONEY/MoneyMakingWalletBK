#region Libraries
using System.Threading.Tasks;
using IDMONEY.IO.Databases;
using IDMONEY.IO.Users;

#endregion
namespace IDMONEY.IO.Infrastructure
{
    public class MySqlNicknameRepository : INicknameRepository
    {
        public async Task<bool> AddAsync(User user, NickName nickName)
        {
            using (var database = new NicknameDatabase())
            {
                return await database.Register(user, nickName);
            }
        }

        public Task<string> GetAByUserAsync(User user)
        {
            //TODO: NEEDS TO BE IMPLEMENTED
            return Task.FromResult("test");
            throw new System.NotImplementedException();
        }
    }
}