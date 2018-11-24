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
    [Route("api/transactions")]
    public class TransactionController : BaseController
    {
        #region Members
        private readonly ITransactionService transactionService;
        #endregion

        #region Constructor
        public TransactionController(ITransactionService transactionService)
        {
            Ensure.IsNotNull(transactionService);

            this.transactionService = transactionService;
        }
        #endregion

        #region Methods
        [HttpPost, Authorize]
        public async Task<Response> InsertTransaction([FromBody]InsertTransactionRequest request)
        {
            request.Transaction.UserId = UserId;

            return await this.transactionService.AddAsync(request);
        }

        [HttpGet, Authorize]
        public async Task<SearchTransactionResponse> SearchTransactionByUser()
        {
            return await this.transactionService.GetUserTransactionsAsync(HttpContext.User);
        }

        //TODO: FindByStatus
        #endregion
    }
}
