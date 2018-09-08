using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDMONEY.IO.Entities
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
