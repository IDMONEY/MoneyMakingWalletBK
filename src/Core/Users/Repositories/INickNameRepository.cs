#region Libraries
using System.Threading.Tasks; 
#endregion

namespace IDMONEY.IO.Users
{
    public interface INicknameRepository
    {
        Task<bool> AddAsync(User user, NickName nickName);
        Task<string> GetAByUserAsync(User user);
    }
}