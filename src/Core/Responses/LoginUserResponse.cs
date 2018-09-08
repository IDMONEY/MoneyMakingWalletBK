using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDMONEY.IO.Responses
{
    public class LoginUserResponse : Response
    {
        public User User { get; set; }

        public string Token { get; set; }
    }
}