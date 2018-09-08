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
        Registered = 1,

        Rejected = 2,

        Processed = 4
    }
}