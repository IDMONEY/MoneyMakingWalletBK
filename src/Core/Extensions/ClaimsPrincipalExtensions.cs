#region Libraries
using System;
using System.Linq;
using System.Security.Claims; 
#endregion

namespace IDMONEY.IO.Security
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
    }
}