using Microsoft.Extensions.DependencyInjection;

namespace NLBank.Infra.Service.Account.Config
{
    public static class Configure
    {
        public static IServiceCollection AddAccountServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            return services;
        }
    }
}
