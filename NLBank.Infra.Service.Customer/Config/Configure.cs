using Microsoft.Extensions.DependencyInjection;
using NLBank.Infra.Service.Customer;

namespace NLBank.Infra.Service.Account.Config
{
    public static class Configure
    {
        public static IServiceCollection AddCustomerServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            return services;
        }
    }
}
