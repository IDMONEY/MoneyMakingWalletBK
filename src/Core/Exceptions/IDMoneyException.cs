#region Libraries
using System;
using System.Runtime.Serialization; 
#endregion

namespace IDMONEY.IO.Exceptions
{
    public class IDMoneyException : Exception
    {
        public IDMoneyException() 
            : base()
        {
        }

        public IDMoneyException(string message) 
            : base(message)
        {
        }

        public IDMoneyException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected IDMoneyException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}