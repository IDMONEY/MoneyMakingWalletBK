﻿#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace IDMONEY.IO.Users
{
    public class User
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public decimal AvailableBalance { get; set; }

        public decimal BlockedBalance { get; set; }

        public string Password { get; set; }

        public string Privatekey { get; set; }
    }
}