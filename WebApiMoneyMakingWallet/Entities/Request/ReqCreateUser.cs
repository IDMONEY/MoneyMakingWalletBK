﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDMONEY.IO.Entities
{
    public class ReqCreateUser
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
