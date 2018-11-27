#region Libraries
using IDMONEY.IO.Accounts;
using IDMONEY.IO.Authorization;
using IDMONEY.IO.Cryptography;
using IDMONEY.IO.Infrastructure;
using IDMONEY.IO.Transactions;
using IDMONEY.IO.Users;
using Microsoft.Extensions.DependencyInjection; 
#endregion

namespace IDMONEY.IO.IoC
{
    public static class NativeInjectorBootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {

            services.AddSingleton<IUserRepository, MySqlUserRepository>();
            services.AddSingleton<IUserService, UserService>();

            services.AddSingleton<ITransactionRepository, MySqlTransactionRepository>();
            services.AddSingleton<ITransactionService, TransactionService>();

            services.AddSingleton<IBusinessRepository, MySqlBusinessRepository>();
            services.AddSingleton<IBusinessService, BusinessService>();

            services.AddSingleton<INicknameRepository, MySqlNicknameRepository>();

            services.AddSingleton<ITokenGenerator, JwtSecurityTokenGenerator>();
            services.AddSingleton<IAuthorizationService, AuthorizationService>();

            services.AddSingleton<IAccountRepository, MySqlAccountRepository>();
        }
    }
}