#region Libraries
using System;
using System.Collections.Generic;
using System.Text; 
#endregion

namespace IDMONEY.IO.Security
{
    public sealed class SecurityContext : ISecurityContext
    {
        #region Constructor
        public SecurityContext(string key, string issuer, string audience)
        {
            //TODO: ENSURE EMPTYS AND NULL
            this.Key = key;
            this.Issuer = issuer;
            this.Audience = audience;
        } 
        #endregion

        #region Properties
        public string Key { get; }
        public string Issuer { get; }
        public string Audience { get; }
        #endregion
    }
}