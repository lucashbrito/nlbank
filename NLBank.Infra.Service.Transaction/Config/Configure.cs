using Microsoft.Extensions.DependencyInjection;

namespace NLBank.Infra.Service.Transaction.Config
{
    public static class Configure
    {
        public static IServiceCollection AddTransactionServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionService, TransactionService>();
            return services;
        }
    }
}
