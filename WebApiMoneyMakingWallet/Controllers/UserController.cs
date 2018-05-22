using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Business;
using IDMONEY.IO.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDMONEY.IO.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        [Route("Create")]
        [HttpPost]
        public ResCreateUser CreateUser([FromBody]ReqCreateUser req)
        {
            BSUser bSUser = new BSUser();
            return bSUser.CreateUser(req);
        }

        [Route("Login")]
        [HttpPost]
        public ResLoginUser Login([FromBody]ReqLoginUser req)
        {
            BSUser bSUser = new BSUser();
            return bSUser.Login(req);
        }
    }
}