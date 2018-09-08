using IDMONEY.IO.Services;
using IDMONEY.IO.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
#region Libraries
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Responses; 
#endregion

namespace IDMONEY.IO.Controllers
{
    [Produces("application/json")]
    [Route("api/Business")]
    public class BusinessController : Controller
    {
        [Route("Search")]
        [HttpPost, Authorize]
        public SearchBusinessResponse SearchBusiness([FromBody]ReqSearchBusiness req)
        {
            BSBusiness bSEntryData = new BSBusiness(HttpContext.User);
            return bSEntryData.SearchBusiness(req);
        }
    }
}
