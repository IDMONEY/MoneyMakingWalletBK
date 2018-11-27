#region Libraries
using IDMONEY.IO.Transactions; 
#endregion

namespace IDMONEY.IO.Requests
{
    public class InsertTransactionRequest : Request
    {
        public InsertTransactionRequest()
        {
            this.Transaction = new Transaction()
            {
                RegistrationDate = SystemTime.Now(),
                Status = TransactionStatus.Registered
            };
        }

        public Transaction Transaction { get; set; }
    }
}