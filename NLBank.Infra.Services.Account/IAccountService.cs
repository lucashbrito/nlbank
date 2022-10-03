namespace NLBank.Infra.Service.Account
{
    public interface IAccountService
    {
        Task<Domain.Account> CreateAsync(string iban, decimal money, Guid CustomerUid);
        Task<Domain.Account> GetByIbanAsync(string iban);
        Task UpdateAsync(Domain.Account account);
    }
}