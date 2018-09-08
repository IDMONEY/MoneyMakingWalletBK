using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDMONEY.IO.Entities
{
    public class BaseResponse
    {
        public bool IsSuccessful { get; set; }

        public List<Error> Errors { get; set; }

        public BaseResponse()
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
