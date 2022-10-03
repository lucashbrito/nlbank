using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NLBank.Infra.Repository.Customer
{
    public static class Configure
    {
        public static IServiceCollection AddCustomerRepository(this IServiceCollection services, string connectionString)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddDbContext<CustomerDatabaseContext>(options =>
                options.UseSqlServer(connectionString,
                    sqlServerOptions =>
                    {
                        sqlServerOptions
                                .MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                        sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }
                       ));

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}