#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace IDMONEY.IO.Transactions
{
    public class Transaction
    {
        #region Properties
        public long? Id { get; set; }
        public long UserId { get; set; }
        public long FromAccountId { get; set; }
        public long ToAccountId { get; set; }
        public decimal? Amount { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? ProcessingDate { get; set; }

        public string Description { get; set; }

        public TransactionStatus Status { get; set; }

        public string StatusName
        {
            get
            {
                if (!Enum.IsDefined(typeof(TransactionStatus), this.Status))
                {
                    return String.Empty;
                }
                return this.Status.GetStringValue();
            }
        }
        #endregion


        #region Methods
        public Transaction ChangeStatus(TransactionStatus status)
        {
            Ensure.IsEnumDefined<TransactionStatus>(status);
            this.Status = status;
            return this;
        }

        public Transaction UpdateAmount(decimal? amount)
        {
            this.Amount = amount;
            return this;
        }


        public Transaction SetProcessingDate(DateTime? processingDate)
        {
            this.ProcessingDate = processingDate;
            return this;
        } 
        #endregion
    }
}
