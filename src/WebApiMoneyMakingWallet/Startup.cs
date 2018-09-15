#region Libraries
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IDMONEY.IO.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json; 
#endregion

namespace IDMONEY.IO
{
    public class Startup
    {
        #region Constructor
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        } 
        #endregion

        #region Properties
        public IConfiguration Configuration { get; } 
        #endregion
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBearerAuthentication(Configuration)
                    .AddWebApi();

            services.Add(new ServiceDescriptor(typeof(DataBaseContext), new DataBaseContext(Configuration.GetConnectionString("DefaultConnection"))));
            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler()
                .UseExceptionHandling()
                .UseAuthentication()
                .UseMvc();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootstrapper.RegisterServices(services);
        }
    }
}
