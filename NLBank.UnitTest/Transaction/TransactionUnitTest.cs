using Moq;
using NLBank.Domain.DomainObjects;

namespace NLBank.UnitTest.Transaction
{
    public class TransactionUnitTest
    {
        private Mock<Infra.Repository.Transaction.ITransactionRepository> transactionRepositoryMock;
        private Infra.Service.Transaction.ITransactionService transactionService;

        public TransactionUnitTest()
        {
            transactionRepositoryMock = new Mock<Infra.Repository.Transaction.ITransactionRepository>();
            transactionService = new Infra.Service.Transaction.TransactionService(transactionRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_CreateAndSaveTransaction()
        {
            var transaction = new Domain.Transaction(Guid.NewGuid(), "NL08001234123412", Domain.Operation.CreateAccount, "Create account");
            transactionRepositoryMock.Setup(x => x.Create(transaction));

            await transactionService.CreateAsync(Guid.NewGuid(), "NL08001234123412", Domain.Operation.CreateAccount, "Create account");

            transactionRepositoryMock.Verify(x => x.Create(transaction), Times.Exactly(1));
            transactionRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Exactly(1));
        }

        [Fact]
        public void Should_CreateTransaction()
        {
            var operationGuid = Guid.NewGuid();
            var iban = "NL08001234123412";
            var operation = Domain.Operation.CreateAccount;
            var details = "Create account";

            var transaction = new Domain.Transaction(operationGuid, iban, operation, details);

            Assert.Equal(details, transaction.Details);
            Assert.Equal(iban, transaction.IBAN);
            Assert.Equal(operation, transaction.Operation);
            Assert.Equal(operationGuid, transaction.OperationId);
            Assert.Equal(details, transaction.Details);
        }

        [Fact]
        public void ShouldNot_CreateTransaction()
        {
            var operationGuid = Guid.NewGuid();
            var iban = string.Empty;
            var operation = Domain.Operation.CreateAccount;
            var details = "Create account";

            var transaction =
            Assert.Throws<DomainException>(() => new Domain.Transaction(operationGuid, iban, operation, details));
        }
    }
}