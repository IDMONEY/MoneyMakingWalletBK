#region Libraries
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using IDMONEY.IO.Responses;
#endregion

namespace IDMONEY.IO.Exceptions
{
    public class NotFoundException : IDMoneyException
    {
        public NotFoundException(Error error) : base(error)
        {
        }

        public NotFoundException(IList<Error> errors) : base(errors)
        {
        }

        public NotFoundException(string message, IList<Error> errors) : base(message, errors)
        {
        }

        public NotFoundException(string message, Exception innerException, IList<Error> errors) : base(message, innerException, errors)
        {
        }

        public NotFoundException(SerializationInfo info, StreamingContext context, IList<Error> errors) : base(info, context, errors)
        {
        }

        public NotFoundException() 
            : base("Not Found")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}