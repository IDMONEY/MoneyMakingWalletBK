#region Libraries
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
#endregion

namespace IDMONEY.IO
{
    public static class WebApiServiceCollectionExtensions
    {
        public static IMvcBuilder AddWebApi(this IServiceCollection services)
        {
            Ensure.IsNotNull(services);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    internalBuilder => internalBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            var builder = services.AddMvcCore();
            services.AddOptions();

            builder.AddJsonFormatters();
            builder.AddJsonOptions(opt =>
            {
                opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                opt.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                opt.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            
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
