#region Libraries
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IDMONEY.IO.Databases;
using IDMONEY.IO.Transactions; 
#endregion

namespace IDMONEY.IO.Infrastructure
{
    public class MySqlBusinessRepository : IBusinessRepository
    {
        public async Task<Business> GetAsync(int id)
        {
            using (var database = new BusinessDatabase())
            {
                return await database.GetBusinessAsync(id);
            }
        }

        public async Task<IList<Business>> FindByNameAsync(string name)
        {
            using (var database = new BusinessDatabase())
            {
                return await database.SearchBusinessAsync(name);
            }
        }

        public async Task<IList<Business>> GetAllAsync()
        {
            using (var database = new BusinessDatabase())
            {
                return await database.SearchBusinessAsync(null);
            }
        }

        public async Task<long> AddAsync(Business business)
        {
            using (var database = new BusinessDatabase())
            {
                return await database.InsertBusinessAsync(business);
            }
        }

        public async Task<IList<Business>> GetByUserAsync(long userId)
        {
            using (var database = new BusinessDatabase())
            {
                return await database.GetBusinessByIdAsync(userId);
            }
        }

    }
}