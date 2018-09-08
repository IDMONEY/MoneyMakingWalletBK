using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Transactions;

namespace IDMONEY.IO.Entities
{
    public class ResSearchTransaction : BaseResponse
    {
        public List<Transaction> Transactions { get; set; }
    }
}
