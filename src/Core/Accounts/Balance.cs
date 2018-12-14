#region Libraries
using System;
using System.Collections.Generic;
using System.Text;

#endregion
namespace IDMONEY.IO.Accounts
{
    public class Balance
    {
        public Balance()
        {
            this.Available = 0;
            this.Blocked = 0;
        }

        public decimal Available{ get; set; }

        public decimal Blocked{ get; set; }
    }
}