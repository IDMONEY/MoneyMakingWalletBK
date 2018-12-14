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
using System.Security.Claims;

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

            return await this.transactionService.AddAsync(request, HttpContext.User);
        }

        [Route("{id:long}")]
        [HttpGet, Authorize]
        public async Task<Response> SearchTransaction(long id)
        {
            Ensure.IsNotNegativeOrZero(id);

            return await this.transactionService.GetAsync(HttpContext.User, id);
        }

        [HttpGet, Authorize]
        [Route("business")]
        public async Task<SearchTransactionResponse> SearchBusinesTransactions()
        {
            return await this.transactionService.GetBusinessTransactionsAsync(HttpContext.User);
        }

        [HttpGet, Authorize]
        [Route("personal")]
        public async Task<SearchTransactionResponse> SearchTransactionByUserAccount()
        {
            return await this.transactionService.GetAccountTransactionsAsync(HttpContext.User);
        }
        
        //TODO: FindByStatus
        #endregion
    }
}
