using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace NLBank.Infra.Repository.Account
{
    public static class Configure
    {
        public static IServiceCollection AddAccountRepository(this IServiceCollection services, string connectionString)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddDbContext<AccountDatabaseContext>(options =>
                   options.UseSqlServer(connectionString,
                    sqlServerOptions =>
                    {
                        sqlServerOptions
                                .MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                        sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }
                       ));
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}