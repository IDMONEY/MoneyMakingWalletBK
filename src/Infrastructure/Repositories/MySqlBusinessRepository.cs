#region Libraries
using System;
using System.Collections.Generic;
using IDMONEY.IO.Databases;
using IDMONEY.IO.Transactions; 
#endregion

namespace IDMONEY.IO.Infrastructure
{
    public class MySqlBusinessRepository : IBusinessRepository
    {
        public Business Get(int id)
        {
            using (var database = new BusinessDatabase())
            {
                return database.GetBusiness(id);
            }
        }

        public IList<Business> FindByName(string name)
        {
            using (var database = new BusinessDatabase())
            {
                return database.SearchBusiness(name);
            }
        }

        public IList<Business> GetAll()
        {
            using (var database = new BusinessDatabase())
            {
                return database.SearchBusiness(null);
            }
        }

        public long Add(Business business)
        {
            using (var database = new BusinessDatabase())
            {
                return database.InsertBusiness(business);
            }
        }

        public IList<Business> GetByUser(long userId)
        {
            using (var database = new BusinessDatabase())
            {
                return database.GetBusinessById(userId);
            }
        }

    }
}