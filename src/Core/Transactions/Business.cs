#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace IDMONEY.IO.Transactions
{
    public class Business
    {
        public string Image { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }

        public decimal AvailableBalance { get; set; }

        public decimal BlockedBalance { get; set; }
    }
}