#region Libraries
using System; 
#endregion

namespace IDMONEY.IO.Transactions
{
    public class TransactionCandidate
    {
        #region Constructor
        public TransactionCandidate(DateTime registrationDateTime)
        {
            this.RegistrationDate = registrationDateTime;
        }
        #endregion

        #region Properties
        public int? BusinessId { get; set; }
        public decimal? Amount { get; set; }
        public int? UserId { get; set; }
        public string Description { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime? RegistrationDate { get;} 
        #endregion

        public static TransactionCandidate Create()
        {
            return new TransactionCandidate(SystemTime.Now())
            {
                Status = TransactionStatus.Registered
            };
        }
    }
}