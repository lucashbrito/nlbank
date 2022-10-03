namespace NLBank.Infra.Repository.Bank
{
    public interface IBankRepository
    {
        void Update(Domain.Bank bank);
        Task<Domain.Bank> GetByBankCodeAsync(string bankCode);
        Task SaveChangesAsync();
        Task<Domain.Bank> Get();
    }
}
