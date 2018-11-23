#region Libraries
using System.Threading.Tasks; 
#endregion

namespace IDMONEY.IO.Users
{
    public interface INicknameRepository
    {
        Task<bool> Create(User user, NickName nickName);
    }
}