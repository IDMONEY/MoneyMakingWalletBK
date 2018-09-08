using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDMONEY.IO.Entities
{
    public class ResCreateUser : BaseResponse
    {
        public User User { get; set; }

        public string Token { get; set; }
    }
}
