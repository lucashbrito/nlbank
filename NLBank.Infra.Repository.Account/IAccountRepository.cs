namespace NLBank.Infra.Repository.Account
{
    public interface IAccountRepository
    {
        void Create(Domain.Account account);
        Task<Domain.Account> GetByIbanAsync(string iban);
        Task SaveChangesAsync();
        Task UpdateAsync(Domain.Account account);
    }
}
