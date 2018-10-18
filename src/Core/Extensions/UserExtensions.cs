#region Libraries
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace IDMONEY.IO.Users
{
    public static class UserExtensions
    {
        public static string GetEncriptedPassword(this User user)
        {
            return $"{user.Email}:{user.Password}".GenerateSHA512();
        }
    }
}