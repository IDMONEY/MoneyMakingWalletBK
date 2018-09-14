#region Libraries
using IDMONEY.IO.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Responses;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Transactions;
#endregion

namespace IDMONEY.IO.Controllers
{
    [Produces("application/json")]
    [Route("api/business")]
    public class BusinessController : Controller
    {

        #region Members
        private readonly IBusinessService businessService;
        #endregion

        public BusinessController(IBusinessService businessService)
        {
            Ensure.IsNotNull(businessService);

            this.businessService = businessService;
        }

        [Route("search")]
        [HttpGet, Authorize]
        public Response SearchBusiness([FromBody]SearchBusinessRequest req)
        {
            BSBusiness bSEntryData = new BSBusiness(HttpContext.User);
            return bSEntryData.SearchBusiness(req);
        }
    }
}