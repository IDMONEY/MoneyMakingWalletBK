#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Accounts;
#endregion

namespace IDMONEY.IO.Transactions
{
    public class Business
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Account Account { get; set; }
    }
}