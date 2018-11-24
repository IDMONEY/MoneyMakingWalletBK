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

        [HttpPost, Authorize]
        public async Task<Response> CreateBusiness([FromBody]CreateBusinessRequest req)
        {
            return await this.businessService.CreateAsync(req);
        }

        [Route("{name:alpha}")]
        [HttpGet, Authorize]
        public async Task<Response> SearchBusiness(string name)
        {
            Ensure.IsNotNullOrEmpty(name);

            return await this.businessService.FindByNameAsync(name);
        }


        [Route("{id:int}")]
        [HttpGet, Authorize]
        public async Task<Response> SearchBusiness(int id)
        {
            Ensure.IsNotNegativeOrZero(id);

            return await this.businessService.GetAsync(id);
        }

        [HttpGet, Authorize]
        public async Task<Response> Get()
        {
            return  await this.businessService.GetAllAsync();
        }


        [HttpGet, Authorize]
        [Route("user")]
        public async Task<Response> GetByUser()
        {
            return await this.businessService.GetByUserAsync(HttpContext.User);
        }
        #endregion
    }
}