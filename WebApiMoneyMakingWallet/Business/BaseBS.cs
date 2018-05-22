using IDMONEY.IO.DataAccess;
using IDMONEY.IO.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IDMONEY.IO.Business
{
    public class BaseBS
    {
        private ClaimsPrincipal ClaimsPrincipal { get; set; }

        public User User { get; private set; }

        public BaseBS(ClaimsPrincipal claimsPrincipal)
        {
            this.ClaimsPrincipal = claimsPrincipal;

            long userId = Convert.ToInt64(this.ClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

            using (DAUser daUser = new DAUser())
            {
                User = daUser.GetUser(userId);
            }
        }

        public BaseBS()
        {

        }

        protected string BuildToken(User userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityContext.KEY));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: SecurityContext.ISSUER,
               audience: SecurityContext.AUDIENCE,
               claims: claims,
               //expires: expiration,
               signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
