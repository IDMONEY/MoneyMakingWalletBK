using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDMONEY.IO.Entities
{
    public class ResInsertTransaction : BaseResponse
    {
        public Transaction Transaction { get; set; }
    }
}
