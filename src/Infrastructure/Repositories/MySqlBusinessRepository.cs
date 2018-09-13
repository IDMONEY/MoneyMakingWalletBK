#region Libraries
using System.Collections.Generic;
using IDMONEY.IO.DataAccess;
using IDMONEY.IO.Transactions; 
#endregion

namespace IDMONEY.IO.Infrastructure
{
    public class MySqlBusinessRepository : IBusinessRepository
    {
        public Business Get(int id)
        {
            using (var database = new DABusiness())
            {
                return database.GetBusiness(id);
            }
        }

        public IList<Business> Get(string name)
        {
            using (var database = new DABusiness())
            {
                return database.SearchBusiness(name);
            }
        }
    }
}