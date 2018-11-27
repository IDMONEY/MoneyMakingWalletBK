#region Libraries
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace IDMONEY.IO.Transactions
{
    public interface IBusinessRepository
    {
        Task<Business> GetAsync(long id);
        Task<long> AddAsync(Business business);
        Task<IList<Business>> FindByNameAsync(string name);
        Task<IList<Business>> GetAllAsync();
        Task<IList<Business>> GetByUserAsync(long userId);
    }
}