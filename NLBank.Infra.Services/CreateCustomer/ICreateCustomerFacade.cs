namespace NLBank.Infra.Services.CreateCustomer
{
    public interface ICreateCustomerFacade
    {
        Task<(Domain.Customer customer, Domain.Account account)> CreateCustomerUsingLibrary(string firstName, string lastName);
    }
}
