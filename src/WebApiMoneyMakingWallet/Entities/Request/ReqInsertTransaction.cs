using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO;
using IDMONEY.IO.Transactions;

namespace IDMONEY.IO.Entities
{
    public class ReqInsertTransaction : BaseRequest
    {
        public Transaction Transaction { get; set; }
    }
}