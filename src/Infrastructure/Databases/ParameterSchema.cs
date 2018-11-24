#region Libraries
using System;
using System.Collections.Generic;
using System.Text;

#endregion
namespace IDMONEY.IO.Databases
{
    public class ParameterSchema
    {
        public string ParamName { get; set; }
        public object ParamValue { get; set; }

        public ParameterSchema() { }

        public ParameterSchema(string ParamName, object ParamValue)
        {
            this.ParamName = ParamName;
            this.ParamValue = ParamValue;
        }
    }
}