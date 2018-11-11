#region Libraries
using System;
using System.Security.Claims;
using IDMONEY.IO.Security;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
using IDMONEY.IO.Users;
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
        public InsertTransactionResponse Add(InsertTransactionRequest request)
        {
            InsertTransactionResponse response = new InsertTransactionResponse();
            response.IsSuccessful = true;

            try
            {
                if (request.Transaction.Amount <= 0)
                {
                    response.IsSuccessful = false;
                    response.Errors.Add(new Error() { Code = ((int)ErrorCodes.InvalidAmount).ToString(), Message = "The amount is invalid" });
                    return response;
                }

                var business = this.businessRepository.Get(request.Transaction.BusinessId.Value);

                if (business.IsNull())
                {
                    response.IsSuccessful = false;
                    response.Errors.Add(new Error() { Code = ((int)ErrorCodes.NotFound).ToString(), Message = "The business was not found" });
                    return response;
                }

                var transactionID = this.transactionRepository.Add(CreateTransaction(request, business));

                var user = this.userRepository.GetById(request.Transaction.UserId.Value);

                //TODO: REDUCE THE NUMBER OF DATABASE ACCESS
                if (user.AvailableBalance >= request.Transaction.Amount)
                {
                    var transaction = this.GetTransaction(transactionID);
                    transaction.ChangeStatus(TransactionStatus.Processed)
                               .UpdateAmount(request.Transaction.Amount)
                               .SetProcessingDate(SystemTime.Now());

                    this.transactionRepository.Update(transaction, user, business);
                }
                else
                {
                    var transaction = this.GetTransaction(transactionID);
                    transaction.ChangeStatus(TransactionStatus.Rejected)
                               .UpdateAmount(null)
                               .SetProcessingDate(SystemTime.Now());

                    this.transactionRepository.Update(transaction);

                    response.IsSuccessful = false;
                    response.Errors.Add(new Error() { Code = ((int)ErrorCodes.InsufficientFunds).ToString(), Message = "The available balance is not enough to make the transaction" });
                    return response;
                }

                response.Transaction = this.GetTransaction(transactionID);
            }
            catch (Exception)
            {

                response.IsSuccessful = false;
                response.Errors.Add(new Error() { Code = ((int)ErrorCodes.Unknown).ToString(), Message = "There was a problem. Please try again later" });
            }

            return response;
        }

        public SearchTransactionResponse GetUserTransactions(ClaimsPrincipal claimsPrincipal)
        {
            SearchTransactionResponse response = new SearchTransactionResponse();
            try
            {
                response.Transactions = this.transactionRepository.GetUserTransactions(claimsPrincipal.GetUserId());
                response.IsSuccessful = true;
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

        private Transaction GetTransaction(long transactionId)
        {
            return this.transactionRepository.Get(transactionId);
        }
        #endregion
    }
}