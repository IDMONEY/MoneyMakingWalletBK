#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Accounts;
#endregion

namespace IDMONEY.IO.Users
{
    public class User
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public Account Account { get; set; }

        public string Password { get; set; }

        public string Privatekey { get; set; }
    }
}