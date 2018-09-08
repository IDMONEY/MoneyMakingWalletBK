using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDMONEY.IO.Entities
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

        public int Status { get; set; }

        public string StatusName { get; set; }
    }
}
