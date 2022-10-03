using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.DependencyInjection;
using NLBank.Infra.Repository.Account;
using NLBank.Infra.Repository.Bank;
using NLBank.Infra.Repository.Customer;
using NLBank.Infra.Repository.Transaction;
using NLBank.Infra.Service.Account.Config;
using NLBank.Infra.Service.Bank.Config;
using NLBank.Infra.Services.CreateCustomer;
using NLBank.Infra.Services.PutMoney;
using NLBank.Infra.Services.TransferMoney;

namespace NLBank.Infra.Service.Transaction.Config
{
    public static class Configure
    {
        public async static Task<IServiceCollection> AddFacadeServicesAsync(this IServiceCollection services)
        {

            var options = new SecretClientOptions()
            {
                Retry = {
            Delay= TimeSpan.FromSeconds(2),
            MaxDelay = TimeSpan.FromSeconds(16),
            MaxRetries = 5,
            Mode = RetryMode.Exponential}
            };

            var client = new SecretClient(new Uri(@"https://studies-dev.vault.azure.net/"), new DefaultAzureCredential(), options);

            services.AddAccountServices();
            services.AddBankServices();
            services.AddTransactionServices();
            services.AddCustomerServices();

            var account = await client.GetSecretAsync("Repositories--Accounts--ConnectionString");
            var banks = await client.GetSecretAsync("Repositories--Banks--ConnectionString");
            var customer = await client.GetSecretAsync("Repositories--Customers--ConnectionString");
            var transaction = await client.GetSecretAsync("Repositories--Transactions--ConnectionString");

            services.AddAccountRepository(account.Value.Value.ToString());
            services.AddBankRepository(banks.Value.Value.ToString());
            services.AddCustomerRepository(customer.Value.Value.ToString());
            services.AddTransactionRepository(transaction.Value.Value.ToString());

            services.AddScoped<ICreateCustomerFacade, CreateCustomerFacade>();
            services.AddScoped<IPutMoneyFacade, PutMoneyFacade>();
            services.AddScoped<ITransferMoneyFacade, TransferMoneyFacade>();
            return services;
        }
    }
}
