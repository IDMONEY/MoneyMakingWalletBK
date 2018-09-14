#region Libraries
using System;
using System.Runtime.Serialization; 
#endregion

namespace IDMONEY.IO.Exceptions
{
    public class UserNotFoundException : IDMoneyException
    {
        public UserNotFoundException() :
            this("User not found")
        {
        }

        public UserNotFoundException(string message) 
            : base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}