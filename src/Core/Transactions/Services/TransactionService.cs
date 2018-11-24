#region Libraries
using System;
using System.Security.Claims;
using IDMONEY.IO.Security;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
using IDMONEY.IO.Users;
using System.Threading.Tasks;
#endregion

namespace IDMONEY.IO.Transactions
{
    public class TransactionService : ITransactionService
    {
        #region Members
        private readonly ITransactionRepository transactionRepository;
        private readonly IBusinessRepository businessRepository;
        private readonly IUserRepository userRepository;
        #endregion

        #region Constructor
        public TransactionService(ITransactionRepository transactionRepository, IBusinessRepository businessRepository, IUserRepository userRepository)
        {
            Ensure.IsNotNull(transactionRepository);
            Ensure.IsNotNull(businessRepository);
            Ensure.IsNotNull(userRepository);

            this.transactionRepository = transactionRepository;
            this.businessRepository = businessRepository;
            this.userRepository = userRepository;
        }

        #endregion

        #region Methods
        public async Task<InsertTransactionResponse> AddAsync(InsertTransactionRequest request)
        {
            InsertTransactionResponse response = new InsertTransactionResponse();

            try
            {
                if (request.Transaction.Amount <= 0)
                {
                    response.IsSuccessful = false;
                    response.Errors.Add(new Error() { Code = ((int)ErrorCodes.InvalidAmount).ToString(), Message = "The amount is invalid" });
                    return response;
                }

                var business = await this.businessRepository.GetAsync(request.Transaction.BusinessId.Value);

                if (business.IsNull())
                {
                    response.IsSuccessful = false;
                    response.Errors.Add(new Error() { Code = ((int)ErrorCodes.NotFound).ToString(), Message = "The business was not found" });
                    return response;
                }

                var transactionID = await this.transactionRepository.AddAsync(CreateTransaction(request, business));

                var user = await this.userRepository.GetByIdAsync(request.Transaction.UserId.Value);

                //TODO: REDUCE THE NUMBER OF DATABASE ACCESS
                if (user.Account.Balance.Available >= request.Transaction.Amount)
                {
                    var transaction = await this.GetTransactionAsync(transactionID);
                    transaction.ChangeStatus(TransactionStatus.Processed)
                               .UpdateAmount(request.Transaction.Amount)
                               .SetProcessingDate(SystemTime.Now());

                    await  this.transactionRepository.UpdateAsync(transaction, user, business);
                }
                else
                {
                    var transaction = await this.GetTransactionAsync(transactionID);
                    transaction.ChangeStatus(TransactionStatus.Rejected)
                               .UpdateAmount(null)
                               .SetProcessingDate(SystemTime.Now());

                    await this.transactionRepository.UpdateAsync(transaction);

                    response.IsSuccessful = false;
                    response.Errors.Add(new Error() { Code = ((int)ErrorCodes.InsufficientFunds).ToString(), Message = "The available balance is not enough to make the transaction" });
                    return response;
                }

                response.Transaction = await this.GetTransactionAsync(transactionID);
            }
            catch (Exception)
            {

                response.IsSuccessful = false;
                response.Errors.Add(new Error() { Code = ((int)ErrorCodes.Unknown).ToString(), Message = "There was a problem. Please try again later" });
            }

            return response;
        }

        public async Task<SearchTransactionResponse> GetUserTransactionsAsync(ClaimsPrincipal claimsPrincipal)
        {
            SearchTransactionResponse response = new SearchTransactionResponse();
            try
            {
                response.Transactions = await this.transactionRepository.GetUserTransactionsAsync(claimsPrincipal.GetUserId());
            }
            catch (Exception)
            {

                response.IsSuccessful = false;
                response.Errors.Add(new Error() { Code = ((int)ErrorCodes.Unknown).ToString(), Message = "There was a problem. Please try again later" });
            }
            return response;
        }
        #endregion

        #region Private Methods
        private TransactionCandidate CreateTransaction(InsertTransactionRequest request, Business business)
        {
            var candidate = TransactionCandidate.Create();
            candidate.UserId = request.Transaction.UserId;
            candidate.Amount = request.Transaction.Amount;
            candidate.Description = request.Transaction.Description;
            candidate.BusinessId = business.Id;
            return candidate;
        }

        private async Task<Transaction> GetTransactionAsync(long transactionId)
        {
            return await this.transactionRepository.GetAsync(transactionId);
        }
        #endregion
    }
}