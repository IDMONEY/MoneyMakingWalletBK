using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Business;
using IDMONEY.IO.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDMONEY.IO.Controllers
{
    [Produces("application/json")]
    [Route("api/EntryData")]
    public class EntryDataController : Controller
    {
        [Route("SearchEntryData")]
        [HttpPost, Authorize]
        public ResSearchEntryData SearchEntryData([FromBody]ReqSearchEntryData req)
        {
            BSEntryData bSEntryData = new BSEntryData(HttpContext.User);
            return bSEntryData.SearchEntryData(req);
        }

        [Route("SaveEntryData")]
        [HttpPost, Authorize]
        public ResSaveEntryData SaveEntryData([FromBody]ReqSaveEntryData req)
        {
            BSEntryData bSEntryData = new BSEntryData(HttpContext.User);
            return bSEntryData.SaveEntryData(req);
        }
        
    }
}