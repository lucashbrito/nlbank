using Moq;
using NLBank.Domain;
using NLBank.Infra.Service.Account;
using NLBank.Infra.Service.Transaction;
using NLBank.Infra.Services.TransferMoney;

namespace NLBank.UnitTest.Services
{
    public class TransferMoneyFacadeUniteTest
    {
        private Mock<IAccountService> accountServiceMock;
        private Mock<ITransactionService> transactionServiceMock;

        private TransferMoneyFacade putMoneyFacade;

        public TransferMoneyFacadeUniteTest()
        {
            accountServiceMock = new Mock<IAccountService>();
            transactionServiceMock = new Mock<ITransactionService>();

            putMoneyFacade = new TransferMoneyFacade(accountServiceMock.Object, transactionServiceMock.Object);
        }

        [Fact]
        public async Task Should_TransferMoney()
        {
            var ibanSent = "NLBUNQ11111111111";
            var ibanReceived = "NLBUNQ00000000002";

            var accountSent = new Domain.Account(ibanSent, 350m, Guid.NewGuid());
            var accountReceived = new Domain.Account(ibanReceived, 1000m, Guid.NewGuid());

            accountServiceMock.Setup(x => x.GetByIbanAsync(ibanSent)).ReturnsAsync(accountSent);
            accountServiceMock.Setup(x => x.GetByIbanAsync(ibanReceived)).ReturnsAsync(accountReceived);

            await putMoneyFacade.TransferMoney(ibanSent, ibanReceived, 250m);

            transactionServiceMock.Verify(x => x.CreateAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Operation>(), It.IsAny<string>()), Times.Exactly(2));
            accountServiceMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Account>()), Times.Exactly(2));

            var moneyLeft = 100m;
            var moneyTotal = 1250m;
            Assert.Equal(moneyLeft, accountSent.Money);
            Assert.Equal(moneyTotal, accountReceived.Money);
        }
    }
}
