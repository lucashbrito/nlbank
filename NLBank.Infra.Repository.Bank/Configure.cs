using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace NLBank.Infra.Repository.Bank
{
    public static class Configure
    {
        public static IServiceCollection AddBankRepository(this IServiceCollection services, string connectionString)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddDbContext<BankDatabaseContext>(options =>
                options.UseSqlServer(connectionString,
                    sqlServerOptions =>
                    {
                        sqlServerOptions
                                .MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                        sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }
                       ));

            services.AddScoped<IBankRepository, BankRepository>();

            return services;
        }
    }
}