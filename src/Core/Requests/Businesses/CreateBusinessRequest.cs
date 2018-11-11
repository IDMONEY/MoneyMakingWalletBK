#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Transactions;
#endregion

namespace IDMONEY.IO.Requests
{
    public class CreateBusinessRequest : Request
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}