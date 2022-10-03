namespace NLBank.Infra.Repository.Customer
{
    public interface ICustomerRepository
    {
        Task<bool> AnyIban(string iban);
        void Create(Domain.Customer customer);
        Task<Domain.Customer> GetByIbanAsync(string iban);
        Task SaveChangesAsync();
    }
}
