namespace NLBank.Infra.Repository.Transaction
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDatabaseContext transactionDatabaseContext;

        public TransactionRepository(TransactionDatabaseContext transactionDatabaseContext)
        {
            this.transactionDatabaseContext = transactionDatabaseContext;
        }
        public void Create(Domain.Transaction transaction)
        {
            transactionDatabaseContext.Transactions.Add(transaction);
        }

        public async Task SaveChangesAsync()
        {
            await transactionDatabaseContext.SaveChangesAsync();
        }
    }
}