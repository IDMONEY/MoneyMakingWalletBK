using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDMONEY.IO.Entities
{
    public class ResGetUser : BaseResponse
    {
        public User User { get; set; }
    }
}
