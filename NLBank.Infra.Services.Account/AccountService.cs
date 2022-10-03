using NLBank.Domain.DomainObjects;
using NLBank.Domain;
using NLBank.Infra.Repository.Account;

namespace NLBank.Infra.Service.Account
{
    public class AccountService : IAccountService
    {
        private IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task<Domain.Account> CreateAsync(string iban, decimal money, Guid CustomerId)
        {
            var account = new Domain.Account(iban, money, CustomerId);

            accountRepository.Create(account);

            await accountRepository.SaveChangesAsync();

            return account;
        }

        public async Task<Domain.Account> GetByIbanAsync(string iban)
        {
            var account = await accountRepository.GetByIbanAsync(iban);

            if (account == null)
                throw new DomainException($"Cannot find the account with this IBAN: {iban}");

            return account;
        }

        public async Task UpdateAsync(Domain.Account account)
        {
            await accountRepository.UpdateAsync(account);
        }
    }
}