#region Libraries
using System;
using System.Collections.Generic;
using System.Text; 
#endregion

namespace IDMONEY.IO.Requests
{
    public abstract class UserRequest : Request
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}