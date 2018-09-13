#region Libraries
using System.Collections.Generic; 
#endregion

namespace IDMONEY.IO.Responses
{
    public abstract class Response
    {
        public bool IsSuccessful { get; set; }

        public List<Error> Errors { get; set; }

        public Response()
        {
            Errors = new List<Error>();
        }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}