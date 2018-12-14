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
        public long FromAccountId { get; set; }
        public long ToAccountId { get; set; }
        public long UserId { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public TransactionStatus Status => TransactionStatus.Registered;
        public DateTime? RegistrationDate { get;}
        #endregion

        #region Static Methods
        public static TransactionCandidate Create()
        {
            return new TransactionCandidate(SystemTime.Now());
        } 
        #endregion
    }
}