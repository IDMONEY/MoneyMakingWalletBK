using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Transactions;

namespace IDMONEY.IO.Responses
{
    public class InsertTransactionResponse : Response
    {
        public Transaction Transaction { get; set; }
    }
}
