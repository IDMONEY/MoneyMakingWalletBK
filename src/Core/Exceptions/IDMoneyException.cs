#region Libraries
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using IDMONEY.IO.Responses;
#endregion

namespace IDMONEY.IO.Exceptions
{
    public class IDMoneyException : Exception
    {
        public IList<Error> Errors { get; set; }

        public IDMoneyException(Error error)
    :    base()
        {
            this.Errors = new List<Error>() { error };
        }

        public IDMoneyException(IList<Error> errors)
            : base()
        {
            this.Errors = errors;
        }

        public IDMoneyException(string message, IList<Error> errors) 
            : base(message)
        {
            this.Errors = errors;
        }

        public IDMoneyException(string message, Exception innerException, IList<Error> errors) 
            : base(message, innerException)
        {
            this.Errors = errors;
        }

        public IDMoneyException(SerializationInfo info, StreamingContext context, IList<Error> errors) 
            : base(info, context)
        {
            this.Errors = errors;
        }

        public IDMoneyException() : this(new List<Error>())
        {
        }

        public IDMoneyException(string message) : this(message, new List<Error>())
        {
        }

        public IDMoneyException(string message, Exception innerException) : this(message, innerException, new List<Error>())
        {
        }

        public IDMoneyException(SerializationInfo info, StreamingContext context) : this(info, context, new List<Error>())
        {
        }
    }
}