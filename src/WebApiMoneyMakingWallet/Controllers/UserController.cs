﻿#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDMONEY.IO.Responses;
using IDMONEY.IO.Users;
using IDMONEY.IO.Requests; 
#endregion

namespace IDMONEY.IO.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UserController : BaseController
    {
        #region Members
        private readonly IUserService userService;
        #endregion

        #region Constructor
        public UserController(IUserService userService)
        {
            Ensure.IsNotNull(userService);

            this.userService = userService;
        }
        #endregion

        #region Methods
        [HttpPost]
        public Response CreateUser([FromBody]CreateUserRequest req)
        {
            return this.userService.Create(req);
        }


        [HttpGet, Authorize]
        public Response GetUser()
        {
            return this.userService.GetUser(HttpContext.User);
        } 
        #endregion
    }
}