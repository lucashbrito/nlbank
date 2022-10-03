namespace NLBank.Infra.Repository.Transaction
{
    public interface ITransactionRepository
    {
        void Create(Domain.Transaction transaction);

        Task SaveChangesAsync();
    }
}
