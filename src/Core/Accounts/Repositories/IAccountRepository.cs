#region Libraries
using System.Threading.Tasks; 
#endregion

namespace IDMONEY.IO.Accounts
{
    public interface IAccountRepository
    {
        Task<Account> GetAsync(long id);
    }
}