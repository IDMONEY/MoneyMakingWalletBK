#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
#endregion

namespace IDMONEY.IO.Extensions
{
    public static class HttpRequestExtensions
    {
        public static bool IsApiCall(this HttpRequest request)
        {
            bool isJson = request.GetTypedHeaders().Accept.Contains(new MediaTypeHeaderValue("application/json"));

            if (isJson)
            {
                return true;
            }
            if (request.Path.Value.StartsWith("/api/"))
            {
                return true;
            }
            return false;
        }
    }
}
