#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace IDMONEY.IO.Transactions
{
    public enum TransactionStatus
    {
        [StringValue("Registered")]
        Registered = 1,

        [StringValue("Rejected")]
        Rejected = 2,

        [StringValue("Processed")]
        Processed = 4
    }
}