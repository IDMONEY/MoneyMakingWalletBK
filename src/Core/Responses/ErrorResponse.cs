#region Libraries
using System;
using System.Collections.Generic;
using System.Text; 
#endregion

namespace IDMONEY.IO.Responses
{
    public class ErrorResponse : Response
    {
        #region Constructor
        public ErrorResponse()
        {
            this.IsSuccessful = false;
        }
        #endregion

        #region Properties
        public string Detail { get; set; }
        #endregion

    }
}