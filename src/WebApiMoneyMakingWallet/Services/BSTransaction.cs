#region Libraries
using IDMONEY.IO.DataAccess;
using IDMONEY.IO.Entities;
using IDMONEY.IO.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
#endregion

namespace IDMONEY.IO.Services
{
    public class BSTransaction : BaseBS
    {
        public BSTransaction(ClaimsPrincipal user) : base(user)
        {
        }

        public ResInsertTransaction InsertTransaction(ReqInsertTransaction req)
        {
            ResInsertTransaction res = new ResInsertTransaction();
            try
            {
                DateTime registrationDate = DateTime.Now;
                long? transactionId = null;
                Business business = null;
                User user = null;

                if (req.Transaction.Amount <= 0)
                {
                    res.IsSuccessful = false;
                    res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.AmountInvalid).ToString(), Message = "The amount is invalid" });
                    return res;
                }

                using (DABusiness da = new DABusiness())
                {
                    business = da.GetBusiness(req.Transaction.BusinessId.Value);
                }

                if (business == null)
                {
                    res.IsSuccessful = false;
                    res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.BusinessNotFound).ToString(), Message = "The business not found" });
                    return res;
                }

                using (DATransaction da = new DATransaction())
                {
                    transactionId = da.InsertTransaction(req.Transaction.BusinessId, User.Id, req.Transaction.Amount, registrationDate, req.Transaction.Description, (int)TransactionStatus.Registered);

                    using (DAUser daUser = new DAUser())
                    {
                        user = daUser.GetUser(User.Id);
                    }

                    DateTime processingDate = DateTime.Now;
                    if (user.AvailableBalance >= req.Transaction.Amount)
                    {
                        da.UpdateTransaction(transactionId, (int)TransactionStatus.Processed, processingDate, req.Transaction.Amount, 
                            business, user);
                    }
                    else
                    {
                        da.UpdateTransaction(transactionId, (int)TransactionStatus.Rejected, processingDate);
                        res.IsSuccessful = false;
                        res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.AvailableBalanceIsEnough).ToString(), Message = "The available balance is not enough to make the transaction" });
                        return res;
                    }

                    res.Transaction = da.GetTransaction(transactionId);
                }

                res.IsSuccessful = true;
                return res;
            }
            catch (Exception ex)
            {
                res.IsSuccessful = false;
                res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.ErrorNotSpecific).ToString(), Message = "There was a problem. Please try again later" });
            }
            return res;
        }

        public ResSearchTransaction SearchTransactionByUser(BaseRequest req)
        {
            ResSearchTransaction res = new ResSearchTransaction();
            using (DATransaction da = new DATransaction())
            {
                res.Transactions = da.SearchTransactionByUser(User.Id);
            }
             res.IsSuccessful = true;
            return res;
        }
    }
}
