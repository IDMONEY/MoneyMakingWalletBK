#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO;
using IDMONEY.IO.Users; 
#endregion

namespace IDMONEY.IO.Responses
{
    public class CreateUserResponse : Response
    {
        public User User { get; set; }

        public string Token { get; set; }
    }
}
