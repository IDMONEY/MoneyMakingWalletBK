#region Libraries
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IDMONEY.IO.Security;
using System.Text;
#endregion

namespace IDMONEY.IO.Cryptography
{
    public class JwtSecurityTokenGenerator : ITokenGenerator
    {
        #region Constants
        private const int MAX_HOURS_TO_BE_EXPIRED = 1;
        private const string AUTHENTICATION_SCHEME = "Bearer";
        #endregion

        #region Members
        private readonly ISecurityContext securityContext;
        #endregion

        #region Constructor
        public JwtSecurityTokenGenerator(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }
        #endregion

        #region Methods
        public string Generate(string value)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, value)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.securityContext.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = SystemTime.Now().AddHours(MAX_HOURS_TO_BE_EXPIRED);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: this.securityContext.Issuer,
               audience: this.securityContext.Audience,
               claims: claims,
               //expires: expiration,
               signingCredentials: creds);

            return $"{AUTHENTICATION_SCHEME} {new JwtSecurityTokenHandler().WriteToken(token)}";
        } 
        #endregion
    }
}