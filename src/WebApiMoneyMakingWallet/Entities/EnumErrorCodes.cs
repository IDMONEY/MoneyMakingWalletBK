#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace IDMONEY.IO.Entities
{
    public enum EnumErrorCodes
    {
        ErrorNotSpecific = 1,
        UserNotFound = 2,
        BusinessNotFound = 3,
        EmailIsRegistred = 4,
        AmountInvalid = 5,
        AvailableBalanceIsEnough = 6
    }
}
