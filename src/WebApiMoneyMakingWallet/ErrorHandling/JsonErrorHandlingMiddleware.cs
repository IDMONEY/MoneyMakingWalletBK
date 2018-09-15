#region Libraries
using IDMONEY.IO.Exceptions;
using IDMONEY.IO.Responses;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
#endregion;

namespace IDMONEY.IO.ErrorHandling
{
    public sealed class JsonErrorHandlingMiddleware
    {
        #region Members
        private readonly RequestDelegate next;
        private readonly IHostingEnvironment environment;
        private readonly ILogger<JsonErrorHandlingMiddleware> logger;
        private readonly JsonSerializer serializer;
        #endregion


        #region Constructor
        public JsonErrorHandlingMiddleware(RequestDelegate next, ILogger<JsonErrorHandlingMiddleware> logger,
            IHostingEnvironment environment)
        {
            Ensure.IsNotNull(logger);

            this.next = next;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.environment = environment;
            this.serializer = new JsonSerializer();
            serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
        #endregion

        #region Methjods
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = this.GetHttpStatus(exception);

                var error = BuildError(exception, environment);

                using (var writer = new StreamWriter(context.Response.Body))
                {
                    serializer.Serialize(writer, error);
                    await writer.FlushAsync().ConfigureAwait(false);
                }
            }
        }
        #endregion

        #region Private Methods
        private static Response BuildError(Exception exception, IHostingEnvironment env)
        {
            var error = new ErrorResponse();

            if (env.IsDevelopment())
            {
                error.Detail = exception.StackTrace;
            }
            else
            {
                error.Detail = exception.Message;
            }

            if (exception is IDMoneyException)
            {
                var internalException = exception as IDMoneyException;
                error.Errors = internalException.Errors;
            }
            return error;
        }

        private int GetHttpStatus(Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var exceptionType = exception.GetType();

            if (exceptionType.Equals(typeof(NotFoundException)))
            {
                statusCode = HttpStatusCode.NotFound;
            }

            return (int)statusCode;
        }
        #endregion
    }
}