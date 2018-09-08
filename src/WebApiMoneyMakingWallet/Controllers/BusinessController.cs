using IDMONEY.IO.Business;
using IDMONEY.IO.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDMONEY.IO.Controllers
{
    [Produces("application/json")]
    [Route("api/Business")]
    public class BusinessController : Controller
    {
        [Route("Search")]
        [HttpPost, Authorize]
        public ResSearchBusiness SearchBusiness([FromBody]ReqSearchBusiness req)
        {
            BSBusiness bSEntryData = new BSBusiness(HttpContext.User);
            return bSEntryData.SearchBusiness(req);
        }
    }
}
