#region Libraries
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace IDMONEY.IO.Accounts
{
    public class Account
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public string PrivateKey { get; set; }
        public AccountType Type { get; set; }
        public Balance Balance { get; set; }
        public string Symbol { get; set; }
    }
}