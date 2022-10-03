using NLBank.Infra.Service.Account;
using NLBank.Infra.Service.Bank;
using NLBank.Infra.Service.Customer;
using NLBank.Infra.Service.Transaction;

namespace NLBank.Infra.Services.CreateCustomer
{
    public class CreateCustomerFacade : ICreateCustomerFacade
    {
        private ICustomerService customerService;
        private IAccountService accountService;
        private ITransactionService transactionService;
        private IBankService bankService;

        public CreateCustomerFacade(ICustomerService customerService,
            IAccountService accountService, ITransactionService transactionService, IBankService bankService)
        {
            this.customerService = customerService;
            this.accountService = accountService;
            this.transactionService = transactionService;
            this.bankService = bankService;
        }

        public async Task<(Domain.Customer customer, Domain.Account account)> CreateCustomerUsingLibrary(string firstName, string lastName)
        {
            var iban = await CreateIBAN();

            var customer = await customerService.CreateAsync(firstName, lastName, iban);

            var trackUid = Guid.NewGuid();

            await transactionService.CreateAsync(trackUid, customer.IBAN, Domain.Operation.CreateCustomer, $"created: {DateTime.Now} any other info");

            var account = await accountService.CreateAsync(customer.IBAN, 0, customer.Id);

            await transactionService.CreateAsync(trackUid, customer.IBAN, Domain.Operation.CreateAccount, $"created: {DateTime.Now} any other info");

            return (customer, account);
        }

        private async Task<string> CreateIBAN()
        {
            string iban = string.Empty;

            bool hasAnyIBAN = true;

            while (hasAnyIBAN)
            {
                iban = await bankService.CreateIBAN();

                if (!await customerService.AnyIban(iban))
                {
                    hasAnyIBAN = false;
                }
            }

            return iban;
        }
    }
}
