using NLBank.Domain.DomainObjects;
using NLBank.Infra.Repository.Bank;
using SinKien.IBAN4Net;

namespace NLBank.Infra.Service.Bank
{
    public class BankService : IBankService
    {
        private IBankRepository bankRepository;
        Random rd;

        public BankService(IBankRepository accountRepository)
        {
            this.bankRepository = accountRepository;
            rd = new Random();
        }

        public async Task<Domain.Bank> GetByIbanAsync(string bankCode)
        {
            var bank = await bankRepository.GetByBankCodeAsync(bankCode);

            if (bank == null)
                throw new DomainException($"Cannot find the bankCode");

            return bank;
        }

        public async Task<string> CreateIBAN()
        {
            var bank = await bankRepository.Get();            

            return new IbanBuilder()
                .CountryCode(CountryCode.GetCountryCode(bank.Country))
                .BankCode(bank.BankCode) 
                .AccountNumberPrefix(rd.Next(2).ToString())
                .AccountNumber(rd.Next(999999999).ToString())
                .Build(true, true).ToString();
        }

        public async Task UpdateAsync(Domain.Bank bank)
        {
            bankRepository.Update(bank);
            await bankRepository.SaveChangesAsync();
        }
    }
}