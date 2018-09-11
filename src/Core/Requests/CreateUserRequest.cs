﻿#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace IDMONEY.IO.Requests
{
    public class CreateUserRequest : Request
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}