using System;
using System.Collections.Generic;
using System.Text;

namespace IDMONEY.IO.Accounts
{
    public enum AccountType
    {
        [StringValue("Business")]
        Business = 'B',

        [StringValue("Personal")]
        Personal = 'P'
    }
}