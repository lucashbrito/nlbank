﻿using Microsoft.EntityFrameworkCore;

namespace NLBank.Infra.Repository.Bank
{
    public class BankRepository : IBankRepository
    {
        private readonly BankDatabaseContext databaseContext;

        public BankRepository(BankDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public void Update(Domain.Bank bank)
        {
            databaseContext.Banks.Update(bank);
        }

        public async Task<Domain.Bank> GetByBankCodeAsync(string bankCode)
            => await databaseContext.Banks.FirstOrDefaultAsync(x => x.BankCode == bankCode);

        public async Task SaveChangesAsync()
        {
            await databaseContext.SaveChangesAsync();
        }

        public async Task<Domain.Bank> Get() => await databaseContext.Banks.FirstOrDefaultAsync();
    }
}