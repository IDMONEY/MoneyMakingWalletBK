#region Libraries
using System;
using System.Security.Claims;
using IDMONEY.IO.Security;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
using IDMONEY.IO.Users;
using System.Threading.Tasks;
using IDMONEY.IO.Accounts;
#endregion

namespace IDMONEY.IO.Transactions
{
    public class TransactionService : ITransactionService
    {
        #region Members
        private readonly ITransactionRepository transactionRepository;
        private readonly IAccountRepository accountRepository;
        private readonly IUserRepository userRepository;
        #endregion

        #region Constructor
        public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IUserRepository userRepository)
        {
            Ensure.IsNotNull(transactionRepository);
            Ensure.IsNotNull(accountRepository);
            Ensure.IsNotNull(userRepository);

            this.transactionRepository = transactionRepository;
            this.accountRepository = accountRepository;
            this.userRepository = userRepository;
        }

        #endregion

        #region Methods
        public async Task<InsertTransactionResponse> AddAsync(InsertTransactionRequest request, ClaimsPrincipal claimsPrincipal)
        {
            InsertTransactionResponse response = new InsertTransactionResponse();

            try
            {
                //TODO: Validate if a the user can add transactions

                if (request.Transaction.Amount <= 0)
                {
                    response.IsSuccessful = false;
                    response.Errors.Add(new Error() { Code = ((int)ErrorCodes.InvalidAmount).ToString(), Message = "The amount is invalid" });
                    return response;
                }

                var fromAccount = await this.accountRepository.GetAsync(request.Transaction.FromAccountId).ConfigureAwait(false);


                if (fromAccount.IsNull())
                {
                    response.IsSuccessful = false;
                    response.Errors.Add(new Error() { Code = ((int)ErrorCodes.NotFound).ToString(), Message = $"Account {request.Transaction.FromAccountId} was not found" });
                    return response;
                }

                var toAccount = await this.accountRepository.GetAsync(request.Transaction.ToAccountId).ConfigureAwait(false);


                if (toAccount.IsNull())
                {
                    response.IsSuccessful = false;
                    response.Errors.Add(new Error() { Code = ((int)ErrorCodes.NotFound).ToString(), Message = $"Account {request.Transaction.ToAccountId} was not found" });
                    return response;
                }



                var user = await this.userRepository.GetByIdAsync(request.Transaction.UserId);

                //TODO: Check if user can register transactions

                var transactionID = await this.transactionRepository.AddAsync(CreateTransaction(request));

               if (fromAccount.Balance.Available >= request.Transaction.Amount)
                {
                    var transaction = await this.GetTransactionAsync(transactionID);
                    transaction.ChangeStatus(TransactionStatus.Processed)
                               .UpdateAmount(request.Transaction.Amount)
                               .SetProcessingDate(SystemTime.Now());

                    await  this.transactionRepository.UpdateAsync(transaction, fromAccount, toAccount);
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

        public async Task<SearchTransactionResponse> GetAccountTransactionsAsync(ClaimsPrincipal claimsPrincipal)
        {
            SearchTransactionResponse response = new SearchTransactionResponse();
            try
            {
                var user = await this.userRepository.GetByIdAsync(claimsPrincipal.GetUserId());

                if (user.Account.IsNotNull())
                {
                    response.Transactions = await this.transactionRepository.GetAccountTransactionsAsync(user.Account);
                }
                else
                {
                    response.Errors.Add(new Error() { Code = ((int)ErrorCodes.NotFound).ToString(), Message = $"Account was not found" });
                }
            }
            catch (Exception exc)
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


        public async Task<TransactionResponse> GetAsync(ClaimsPrincipal claimsPrincipal, long id)
        {
            TransactionResponse response = new TransactionResponse();
            try
            {
                //TODO: Check if the user has access to the transaction
                response.Transaction = await this.GetTransactionAsync(id);
                
            }
            catch (Exception exc)
            {

                
            }
            return response;
        }
        #endregion

        #region Private Methods
        private TransactionCandidate CreateTransaction(InsertTransactionRequest request)
        {
            var candidate = TransactionCandidate.Create();
            candidate.FromAccountId = request.Transaction.FromAccountId;
            candidate.Amount = request.Transaction.Amount;
            candidate.Description = request.Transaction.Description;
            candidate.ToAccountId = request.Transaction.ToAccountId;
            candidate.UserId = request.Transaction.UserId;
            return candidate;
        }

        private async Task<Transaction> GetTransactionAsync(long transactionId)
        {
            return await this.transactionRepository.GetAsync(transactionId);
        }
        #endregion
    }
}