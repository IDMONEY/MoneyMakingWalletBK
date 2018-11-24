#region Libraries
using System.Collections.Generic; 
#endregion

namespace IDMONEY.IO.Responses
{
    public abstract class Response
    {
        public bool IsSuccessful { get; set; }

        public IList<Error> Errors { get; set; }

        protected Response()
        {
            Errors = new List<Error>();
            this.IsSuccessful = true;
        }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}