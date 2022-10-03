namespace NLBank.Infra.Service.Bank
{
    public interface IBankService
    {
        Task UpdateAsync(Domain.Bank bank);
        Task<string> CreateIBAN();
        Task<Domain.Bank> GetByIbanAsync(string bankCode);
    }
}