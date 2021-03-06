﻿#region Libraries
using IDMONEY.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using IDMONEY.IO.Exceptions;
#endregion

namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAuthenticated(this ClaimsPrincipal user)
        {
            return user?.Identity?.IsAuthenticated == true;
        }

        public static bool HasClaims(this ClaimsPrincipal user, params Predicate<Claim>[] requiredClaims)
        {
            return user.IsNotNull()
                && requiredClaims.All(user.HasClaim);
        }

        public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

            if (claim.IsNotNull())
            {
                return Convert.ToInt64(claim.Value);
            }

            throw new NotFoundException("User not found");
        }
    }
}