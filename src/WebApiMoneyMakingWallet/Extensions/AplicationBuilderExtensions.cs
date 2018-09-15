#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.ErrorHandling;
using Microsoft.AspNetCore.Builder; 
#endregion

namespace IDMONEY.IO
{
    public static class AplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            Ensure.IsNotNull(app);

            return app.UseMiddleware<JsonErrorHandlingMiddleware>();
        }
    }
}