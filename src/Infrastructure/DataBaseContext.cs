#region Libraries
using System;
using System.Collections.Generic;
using System.Text;

#endregion
namespace IDMONEY.IO
{
    public class DataBaseContext
    {
        public static string CONNECTION_STRING { get; set; }
        public static string PROVIDER_NAME { get; set; }

        public DataBaseContext(string connectionString, string providerName)
        {
            CONNECTION_STRING = connectionString;
            PROVIDER_NAME = providerName;
        }
    }
}