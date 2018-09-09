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

#endregion
namespace IDMONEY.IO.Controllers
{
    [Produces("application/json")]
    [Route("api/transaction")]
    public class TransactionController : Controller
    {
        [HttpPost, Authorize]
        public InsertTransactionResponse InsertTransaction([FromBody]InsertTransactionRequest req)
        {
            BSTransaction bSTransaction = new BSTransaction(HttpContext.User);
            return bSTransaction.InsertTransaction(req);
        }

        [Route("SearchByUser")]
        [HttpPost, Authorize]
        public SearchTransactionResponse SearchTransactionByUser([FromBody]Request req)
        {
            BSTransaction bSTransaction = new BSTransaction(HttpContext.User);
            return bSTransaction.SearchTransactionByUser(req);
        }
    }
}
