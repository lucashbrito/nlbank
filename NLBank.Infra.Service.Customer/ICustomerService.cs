namespace NLBank.Infra.Service.Customer
{
    public interface ICustomerService
    {
        Task<bool> AnyIban(string iban);
        Task<Domain.Customer> CreateAsync(string firstName, string lastName, string iban);
        Task<Domain.Customer> GetByIbanAsync(string iban);
    }
}