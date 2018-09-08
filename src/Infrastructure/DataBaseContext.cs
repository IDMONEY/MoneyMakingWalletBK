using System;
using System.Collections.Generic;
using System.Text;

namespace IDMONEY.IO
{
    public class DataBaseContext
    {
        public static string CONNECTION_STRING { get; set; }

        public DataBaseContext(string connectionString)
        {
            DataBaseContext.CONNECTION_STRING = connectionString;
        }
    }
}