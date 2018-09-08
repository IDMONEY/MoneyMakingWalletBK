using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Services;
using IDMONEY.IO.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDMONEY.IO.Responses;

namespace IDMONEY.IO.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        [Route("Create")]
        [HttpPost]
        public CreateUserResponse CreateUser([FromBody]ReqCreateUser req)
        {
            BSUser bSUser = new BSUser();
            return bSUser.CreateUser(req);
        }

        [Route("Login")]
        [HttpPost]
        public LoginUserResponse Login([FromBody]ReqLoginUser req)
        {
            BSUser bSUser = new BSUser();
            return bSUser.Login(req);
        }


        [Route("Get")]
        [HttpPost, Authorize]
        public UserResponse GetUser(BaseRequest req)
        {
            BSUser bSUser = new BSUser(HttpContext.User);
            return bSUser.GetUser(req);
        }
    }
}