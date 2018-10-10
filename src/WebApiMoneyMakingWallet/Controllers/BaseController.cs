using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace IDMONEY.IO.Controllers
{
    public class BaseController : Controller
    {
        public int? UserId
        {
            get
            {
                int? userId = null;
                if (HttpContext.User.IsNotNull() && HttpContext.User.Claims.IsNotNull())
                {
                    var user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
                    if (user.IsNotNull())
                    {
                        userId = Convert.ToInt32(user.Value);
                    }
                }
                return userId;
            }
        }
    }
}
