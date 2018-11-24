#region Libraries
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
        public async Task<Response> CreateUser([FromBody]CreateUserRequest req)
        {
            return await this.userService.CreateAsync(req);
        }


        [HttpGet, Authorize]
        public async Task<Response> GetUser()
        {
            return await this.userService.GetUserAsync(HttpContext.User);
        } 
        #endregion
    }
}