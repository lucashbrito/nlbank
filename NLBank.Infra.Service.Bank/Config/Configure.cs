using Microsoft.Extensions.DependencyInjection;

namespace NLBank.Infra.Service.Bank.Config
{
    public static class Configure
    {
        public static IServiceCollection AddBankServices(this IServiceCollection services)
        {
            services.AddScoped<IBankService, BankService>();
            return services;
        }
    }
}
