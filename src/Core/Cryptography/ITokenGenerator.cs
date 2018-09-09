#region Libraries
using System;
using System.Collections.Generic;
using System.Text; 
#endregion

namespace IDMONEY.IO.Cryptography
{
    public interface ITokenGenerator
    {
        string Generate(string value);
    }
}