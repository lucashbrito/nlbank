using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace NLBank.Infra.Repository.Transaction
{
    public static class Configure
    {
        public static IServiceCollection AddTransactionRepository(this IServiceCollection services, string connectionString)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddDbContext<TransactionDatabaseContext>(options =>
                  options.UseSqlServer(connectionString,
                    sqlServerOptions =>
                    {
                        sqlServerOptions
                                .MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                        sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }
                       ));

            services.AddScoped<ITransactionRepository, TransactionRepository>();

            return services;
        }
    }
}