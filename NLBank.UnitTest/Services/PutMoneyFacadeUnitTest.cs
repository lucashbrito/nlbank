using Moq;
using NLBank.Domain;
using NLBank.Infra.Service.Account;
using NLBank.Infra.Service.Bank;
using NLBank.Infra.Service.Transaction;
using NLBank.Infra.Services.PutMoney;

namespace NLBank.UnitTest.Services
{
    public class PutMoneyFacadeUnitTest
    {
        private Mock<IAccountService> accountServiceMock;
        private Mock<ITransactionService> transactionServiceMock;
        private Mock<IBankService> bankServiceMock;

        private IPutMoneyFacade putMoneyFacade;

        public PutMoneyFacadeUnitTest()
        {
            accountServiceMock = new Mock<IAccountService>();
            transactionServiceMock = new Mock<ITransactionService>();
            bankServiceMock = new Mock<IBankService>();

            putMoneyFacade = new PutMoneyFacade(accountServiceMock.Object, transactionServiceMock.Object, bankServiceMock.Object);
        }

        [Fact]
        public async Task Should_PutMoney()
        {
            var iban = "NLBUNQ12345678910";
            var firstName = "Lucas";
            var lastName = "Brito";
            var customer = new Domain.Customer(firstName, lastName, iban);
            var account = new Domain.Account(iban, 0, customer.Id);
            var bank = new Domain.Bank(iban, 10000, "NL");

            accountServiceMock.Setup(x => x.GetByIbanAsync(iban)).ReturnsAsync(account);
            bankServiceMock.Setup((x) => x.GetByIbanAsync(account.GetBankCode())).ReturnsAsync(bank);

            await putMoneyFacade.PutMoney(iban, 250m);

            transactionServiceMock.Verify(x => x.CreateAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Operation>(), It.IsAny<string>()), Times.Exactly(2));
            accountServiceMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Account>()), Times.Exactly(1));
            bankServiceMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Bank>()), Times.Exactly(1));

            var feeApplied = 10002.50M;
            var moneyReceived = 247.50m;
            Assert.Equal(feeApplied, bank.Money);
            Assert.Equal(moneyReceived, account.Money);
        }
    }
}
