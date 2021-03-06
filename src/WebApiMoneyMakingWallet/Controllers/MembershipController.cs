﻿#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO;
using IDMONEY.IO.Authorization;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
using IDMONEY.IO.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
#endregion

namespace IDMONEY.IO.Controllers
{
    [Produces("application/json")]
    [Route("api/membership")]
    public class MembershipController : BaseController
    {
        #region Members
        private readonly IAuthorizationService authorizationService;
        #endregion


        #region Constructor
        public MembershipController(IAuthorizationService authorizationService)
        {
            Ensure.IsNotNull(authorizationService);

            this.authorizationService = authorizationService;
        }
        #endregion

        #region Methods
        [Route("login")]
        [HttpPut]
        [HttpPost]
        public async Task<Response> Login([FromBody]LoginUserRequest request)
        {
            return await authorizationService.AuthorizeAsync(request);
        } 
        #endregion
    }
}