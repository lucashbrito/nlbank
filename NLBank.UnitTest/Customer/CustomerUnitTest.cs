using Moq;
using NLBank.Infra.Repository.Customer;
using NLBank.Infra.Service.Customer;

namespace NLBank.UnitTest.Customer
{
    public class CustomerUnitTest
    {
        private Mock<ICustomerRepository> customerRepositoryMock;
        private ICustomerService customerService;

        public CustomerUnitTest()
        {
            customerRepositoryMock = new Mock<ICustomerRepository>();
            customerService = new CustomerService(customerRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_CreateCustomer()
        {
            var firsName = "lucas";
            var lastName = "brito";
            var iban = "NLBUNQ1234567890";
            var customer = new Domain.Customer(firsName, lastName, iban);

            customerRepositoryMock.Setup(x => x.Create(customer));
            customerRepositoryMock.Setup(x => x.SaveChangesAsync());

            await customerService.CreateAsync(firsName, lastName, iban);

            customerRepositoryMock.Verify(x => x.Create(customer), Times.Exactly(1));
            customerRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Exactly(1));
        }
    }
}
