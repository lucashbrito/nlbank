using Moq;
using NLBank.Domain.DomainObjects;

namespace NLBank.UnitTest.Account
{
    public class AccountUnitTest
    {
        private Mock<Infra.Repository.Account.IAccountRepository> accountRepositoryMock;
        private Infra.Service.Account.IAccountService accountService;

        public AccountUnitTest()
        {
            accountRepositoryMock = new Mock<Infra.Repository.Account.IAccountRepository>();
            accountService = new Infra.Service.Account.AccountService(accountRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_CreateAndSaveAccount()
        {
            var customerUid = Guid.NewGuid();
            var iban = "NL08001234123412";
            decimal money = 12.2m;

            var account = new Domain.Account(iban, money, customerUid);

            accountRepositoryMock.Setup(x => x.Create(account));

            await accountService.CreateAsync(iban, money, customerUid);

            accountRepositoryMock.Verify(x => x.Create(account), Times.Exactly(1));
            accountRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Exactly(1));
        }

        [Fact]
        public void Should_CreateAccount()
        {
            var customerUid = Guid.NewGuid();
            var iban = "NL08001234123412";
            decimal money = 12.2m;

            var account = new Domain.Account(iban, money, customerUid);

            Assert.Equal(customerUid, account.CustomerId);
            Assert.Equal(iban, account.IBAN);
            Assert.Equal(money, account.Money);
        }

        [Fact]
        public void Should_CreateTransferMoney()
        {
            var customerUid = Guid.NewGuid();
            var customerUidReceive = Guid.NewGuid();
            var iban = "NL08001234123412";
            var ibanReceive = "NL08001234123412";


            var accountsend = new Domain.Account(iban, 100, customerUid);

            var accountReceive = new Domain.Account(ibanReceive, 50, customerUidReceive);

            accountsend.TransferMoney(accountReceive, 50);

            Assert.Equal(iban, accountsend.IBAN);
            Assert.Equal(50, accountsend.Money);

            Assert.Equal(ibanReceive, accountReceive.IBAN);
            Assert.Equal(100, accountReceive.Money);
        }

        [Fact]
        public void ShouldNot_CreateAccount()
        {
            var customerUid = Guid.NewGuid();
            var iban = string.Empty;
            decimal money = 12.2m;

            Assert.Throws<DomainException>(() => new Domain.Account(iban, money, customerUid));
        }
    }
}