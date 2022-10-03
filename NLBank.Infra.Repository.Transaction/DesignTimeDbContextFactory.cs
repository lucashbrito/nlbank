using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Rest.TransientFaultHandling;

namespace NLBank.Infra.Repository.Transaction
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TransactionDatabaseContext>
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Local.json", optional: false, reloadOnChange: true)
            .Build();

        public TransactionDatabaseContext CreateDbContext(string[] args)
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(
                    azureServiceTokenProvider.KeyVaultTokenCallback));

            keyVaultClient.SetRetryPolicy(new RetryPolicy<HttpStatusCodeErrorDetectionStrategy>(
                new FixedIntervalRetryStrategy(10, TimeSpan.FromSeconds(2))));

            var config = new ConfigurationBuilder()
                .AddAzureKeyVault(new AzureKeyVaultConfigurationOptions()
                {
                    Client = keyVaultClient,
                    Manager = new DefaultKeyVaultSecretManager(),
                    Vault = Configuration["Azure:KeyVault:Address"]
                }).Build();

            return new TransactionDatabaseContext(
                new DbContextOptionsBuilder<TransactionDatabaseContext>()
             .UseSqlServer(config["Repositories:Transactions:ConnectionString"]).Options);
        }
    }
}
