using IDMONEY.IO.Services;
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
    [Route("api/Transaction")]
    public class TransactionController : Controller
    {
        [Route("Insert")]
        [HttpPost, Authorize]
        public ResInsertTransaction InsertTransaction([FromBody]ReqInsertTransaction req)
        {
            BSTransaction bSTransaction = new BSTransaction(HttpContext.User);
            return bSTransaction.InsertTransaction(req);
        }

        [Route("SearchByUser")]
        [HttpPost, Authorize]
        public ResSearchTransaction SearchTransactionByUser([FromBody]BaseRequest req)
        {
            BSTransaction bSTransaction = new BSTransaction(HttpContext.User);
            return bSTransaction.SearchTransactionByUser(req);
        }
    }
}
