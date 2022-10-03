using NLBank.Domain;
using NLBank.Infra.Repository.Transaction;

namespace NLBank.Infra.Service.Transaction
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public async Task CreateAsync(Guid operationUid, string iban, Operation operation, string details)
        {
            transactionRepository.Create(new Domain.Transaction(operationUid, iban, operation, details));

            await transactionRepository.SaveChangesAsync();
        }
    }
}