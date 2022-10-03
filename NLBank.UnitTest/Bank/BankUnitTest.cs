using Moq;
using NLBank.Domain.DomainObjects;
using NLBank.Domain;
using SinKien.IBAN4Net;

namespace NLBank.UnitTest.Account
{
    public class BankUnitTest
    {
        private Mock<Infra.Repository.Bank.IBankRepository> bankRepositoryMock;
        private Infra.Service.Bank.IBankService bankService;

        public BankUnitTest()
        {
            bankRepositoryMock = new Mock<Infra.Repository.Bank.IBankRepository>();
            bankService = new Infra.Service.Bank.BankService(bankRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_UpdateBank()
        {
            var bankCode = "13245";
            decimal money = 12.2m;

            var bank = new Domain.Bank(bankCode, money, "NL");

            bankRepositoryMock.Setup(x => x.Update(bank));

            await bankService.UpdateAsync(bank);

            bankRepositoryMock.Verify(x => x.Update(bank), Times.Exactly(1));
            bankRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Exactly(1));
        }

        [Fact]
        public void Should_CallFee_AddMoney()
        {
            var bankCode = "13245";
            decimal money = 12.2m;

            var bank = new Domain.Bank(bankCode, money, "NL");

            var customerMoneyLeft = bank.Fee(10m);

            Assert.Equal(bankCode, bank.BankCode);
            Assert.Equal(12.30m, bank.Money);
            Assert.Equal(9.90m, customerMoneyLeft);
        }

        [Fact]
        public void Should_CallFee_AddNoMoney()
        {
            var bankCode = "13245";
            decimal money = 12.2m;

            var bank = new Domain.Bank(bankCode, money, "NL");

            Assert.Throws<DomainException>(() => bank.Fee(-112m));
        }
    }
}