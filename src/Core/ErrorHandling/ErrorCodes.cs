#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace IDMONEY.IO
{
    public enum ErrorCodes
    {
        Unknown = 1,
        NotFound = 2,
        EmailAlreadyRegistred = 4,
        InvalidAmount = 5,
        AvailableBalanceIsEnough = 6
    }
}