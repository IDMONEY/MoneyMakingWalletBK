#region Libraries
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
#endregion

namespace IDMONEY.IO
{
    public static class WebApiServiceCollectionExtensions
    {
        public static IMvcBuilder AddWebApi(this IServiceCollection services)
        {
            Ensure.IsNotNull(services);

            var builder = services.AddMvcCore();
            builder.AddJsonFormatters();
            builder.AddJsonOptions(opt =>
            {
               opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                opt.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                opt.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            builder.AddCors();

            return new MvcBuilder(builder.Services, builder.PartManager);
        }

        public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<MvcOptions> setupAction)
        {
            Ensure.IsNotNull(services);
            Ensure.IsNotNull(setupAction);

            var builder = services.AddWebApi();
            builder.Services.Configure(setupAction);

            return builder;
        }
    }
}
