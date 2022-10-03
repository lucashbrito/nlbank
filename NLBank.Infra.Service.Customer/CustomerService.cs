using NLBank.Infra.Repository.Customer;

namespace NLBank.Infra.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<bool> AnyIban(string iban)
        {
            return await customerRepository.AnyIban(iban);
        }

        public async Task<Domain.Customer> CreateAsync(string firstname, string lastName, string iban)
        {
            var customer = new Domain.Customer(firstname, lastName, iban);

            customerRepository.Create(customer);

            await customerRepository.SaveChangesAsync();

            return customer;
        }

        public async Task<Domain.Customer> GetByIbanAsync(string iban)
        {
            return await customerRepository.GetByIbanAsync(iban);
        }
    }
}