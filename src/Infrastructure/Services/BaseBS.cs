#region Libraries
using IDMONEY.IO.Databases;
using IDMONEY.IO.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks; 
#endregion

namespace IDMONEY.IO.Services
{
    public abstract class BaseBS
    {
        private ClaimsPrincipal ClaimsPrincipal { get; set; }

        public User User { get; private set; }

        public BaseBS(ClaimsPrincipal claimsPrincipal)
        {
            this.ClaimsPrincipal = claimsPrincipal;

            long userId = Convert.ToInt64(this.ClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

            using (UserDatabase daUser = new UserDatabase())
            {
                User = daUser.GetUser(userId);
            }
        }

        public BaseBS()
        {

        }
    }
}
