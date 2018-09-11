using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDMONEY.IO.Cryptography;
using IDMONEY.IO.Infrastructure;
using IDMONEY.IO.Security;
using IDMONEY.IO.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IDMONEY.IO
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var tokenParams = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidIssuer = Configuration["JWT:Issuer"],
                ValidAudience = Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"]))
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtconfig =>
                {
                    jwtconfig.TokenValidationParameters = tokenParams;
                });

            services.AddMvc();

            services.Add(new ServiceDescriptor(typeof(DataBaseContext), new DataBaseContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.AddSingleton<ISecurityContext>(new SecurityContext(Configuration["JWT:key"], Configuration["JWT:Issuer"], Configuration["JWT:Audience"]));
            services.AddSingleton<IUserRepository, MySqlUserRepository>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IAuthorizationService, AuthorizationService>();
            services.AddSingleton<ITokenGenerator, JwtSecurityTokenGenerator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }

    /*public class SecurityContext
    {
        public static string KEY { get; set; }

        public static string ISSUER { get; set; }

        public static string AUDIENCE { get; set; }

        public SecurityContext(string key, string issuer, string audience)
        {
            SecurityContext.KEY = key;
            SecurityContext.ISSUER = issuer;
            SecurityContext.AUDIENCE = audience;
        }
    }*/
}
