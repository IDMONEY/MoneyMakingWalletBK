#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
using IDMONEY.IO.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
#endregion

namespace WebApiMoneyMakingWallet.Controllers
{
    [Produces("application/json")]
    [Route("api/membership")]
    public class MembershipController : Controller
    {
        #region Members
        private readonly IAuthorizationService authorizationService;
        #endregion


        #region Constructor
        public MembershipController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }
        #endregion

        [Route("login")]
        [HttpPut]
        public LoginUserResponse Login([FromBody]LoginUserRequest request)
        {
            return authorizationService.Authorize(request);
        }
    }
}