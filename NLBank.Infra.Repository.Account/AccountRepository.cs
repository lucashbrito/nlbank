using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace NLBank.Infra.Repository.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDatabaseContext databaseContext;

        public AccountRepository(AccountDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public void Create(Domain.Account account)
        {
            databaseContext.Accounts.Add(account);
        }

        public async Task<Domain.Account> GetByIbanAsync(string iban)
            => await databaseContext.Accounts.FirstOrDefaultAsync(x => x.IBAN == iban);

        public async Task SaveChangesAsync()
        {
            await databaseContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Account account)
        {
            databaseContext.Accounts.Update(account);
            await SaveChangesAsync();
        }
    }
}