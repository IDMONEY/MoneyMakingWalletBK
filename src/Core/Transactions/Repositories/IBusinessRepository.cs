#region Libraries
using System;
using System.Collections.Generic;
using System.Text; 
#endregion

namespace IDMONEY.IO.Transactions
{
    public interface IBusinessRepository
    {
        Business Get(int id);
        IList<Business> FindByName(string name);
        IList<Business> GetAll ();
    }
}