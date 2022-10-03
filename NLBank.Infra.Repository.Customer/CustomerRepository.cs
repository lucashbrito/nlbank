using Microsoft.EntityFrameworkCore;

namespace NLBank.Infra.Repository.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDatabaseContext databaseContext;

        public CustomerRepository(CustomerDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<bool> AnyIban(string iban)
            => await databaseContext.Customers.AnyAsync(x => x.IBAN == iban);

        public void Create(Domain.Customer customer)
        {
            databaseContext.Customers.Add(customer);
        }

        public async Task<Domain.Customer> GetByIbanAsync(string iban)
            => await databaseContext.Customers.FirstOrDefaultAsync(x => x.IBAN == iban);

        public async Task SaveChangesAsync()
        {
            await databaseContext.SaveChangesAsync();
        }
    }
}