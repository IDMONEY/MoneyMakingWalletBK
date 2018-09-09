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

namespace IDMONEY.IO.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        #region Members
        private readonly IUserService userService; 
        #endregion


        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public CreateUserResponse CreateUser([FromBody]CreateUserRequest req)
        {
            return this.userService.Create(req);
        }

        [Route("login")]
        [HttpPost]
        public LoginUserResponse Login([FromBody]LoginUserRequest req)
        {
            BSUser bSUser = new BSUser();
            return bSUser.Login(req);
        }


        [HttpGet]
        public UserResponse GetUser(Request req)
        {
            BSUser bSUser = new BSUser(HttpContext.User);
            return bSUser.GetUser(req);
        }
    }
}