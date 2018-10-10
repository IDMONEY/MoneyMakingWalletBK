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
    [Route("api/businesses")]
    public class BusinessController : BaseController
    {

        #region Members
        private readonly IBusinessService businessService;
        #endregion

        #region Constructor
        public BusinessController(IBusinessService businessService)
        {
            Ensure.IsNotNull(businessService);

            this.businessService = businessService;
        }
        #endregion

        #region Methods

        [Route("{name:alpha}")]
        [HttpGet, Authorize]
        public Response SearchBusiness(string name)
        {
            Ensure.IsNotNullOrEmpty(name);
            return this.businessService.FindByName(name);
        }


        [Route("{id:int}")]
        [HttpGet, Authorize]
        public Response SearchBusiness(int id)
        {
            Ensure.IsNotNegativeOrZero(id);
            return this.businessService.Get(id);
        }

        [HttpGet, Authorize]
        public Response Get()
        {
            return this.businessService.GetAll();
        }
        #endregion
    }
}