#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace IDMONEY.IO.Responses
{
    public class UserResponse : Response
    {
        public User User { get; set; }
    }
}