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
        public long? Id { get; set; }

        public int? BusinessId { get; set; }

        public string BusinessName { get; set; }

        public string Image { get; set; }

        public int? UserId { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? ProcessingDate { get; set; }

        public string Description { get; set; }

        public TransactionStatus Status { get; set; }

        public string StatusName { get; set; }


        public Transaction ChangeStatus(TransactionStatus status)
        {
            //TODO CHECK IF ENUM IS DEFINED;
            this.Status = status;
            return this;
        }

        public Transaction UpdateAmount(decimal? amount)
        {
            //TODO CHECK IF ENUM IS DEFINED;
            this.Amount = amount;
            return this;
        }
        

        public Transaction SetProcessingDate(DateTime? processingDate)
        {
            //TODO CHECK IF ENUM IS DEFINED;
            this.ProcessingDate = processingDate;
            return this;
        }
    }
}
