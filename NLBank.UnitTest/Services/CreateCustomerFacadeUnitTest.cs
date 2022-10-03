using Moq;
using NLBank.Domain;
using NLBank.Infra.Service.Account;
using NLBank.Infra.Service.Bank;
using NLBank.Infra.Service.Customer;
using NLBank.Infra.Service.Transaction;
using NLBank.Infra.Services.CreateCustomer;

namespace NLBank.UnitTest.Services
{
    public class CreateCustomerFacadeUnitTest
    {
        private Mock<ICustomerService> customerServiceMock;
        private Mock<IAccountService> accountServiceMock;
        private Mock<ITransactionService> transactionServiceMock;
        private Mock<IBankService> bankServiceMock;


        private ICreateCustomerFacade createCustomerFacade;

        public CreateCustomerFacadeUnitTest()
        {
            customerServiceMock = new Mock<ICustomerService>();
            accountServiceMock = new Mock<IAccountService>();
            transactionServiceMock = new Mock<ITransactionService>();
            bankServiceMock = new Mock<IBankService>();

            createCustomerFacade = new CreateCustomerFacade(customerServiceMock.Object, accountServiceMock.Object,
                transactionServiceMock.Object, bankServiceMock.Object);
        }

        [Fact]
        public async Task Should_CreateCustomer()
        {
            var iban = "NLBUNQ12345678910";
            var firstName = "Lucas";
            var lastName = "Brito";
            var customer = new Domain.Customer(firstName, lastName, iban);
            var account = new Domain.Account(iban, 0, customer.Id);

            bankServiceMock.Setup((x) => x.CreateIBAN()).ReturnsAsync(iban);
            customerServiceMock.Setup(x => x.CreateAsync(firstName, lastName, iban)).ReturnsAsync(customer);
            customerServiceMock.Setup(x => x.AnyIban(iban)).ReturnsAsync(false);
            accountServiceMock.Setup(x => x.CreateAsync(iban, 0, customer.Id)).ReturnsAsync(account);

            (var customerCreadted, var accountCreated) = await createCustomerFacade.CreateCustomerUsingLibrary(firstName, lastName);

            transactionServiceMock.Verify(x => x.CreateAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Operation>(), It.IsAny<string>()), Times.Exactly(2));
            customerServiceMock.Verify(x => x.CreateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
            accountServiceMock.Verify(x => x.CreateAsync(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<Guid>()), Times.Exactly(1));
        }
    }
}
