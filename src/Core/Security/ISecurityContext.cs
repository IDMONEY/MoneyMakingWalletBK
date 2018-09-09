using System;
using System.Collections.Generic;
using System.Text;

namespace IDMONEY.IO.Security
{
    public interface ISecurityContext
    {
        string Key { get; }
        string Issuer { get; }
        string Audience { get; }
    }
}
