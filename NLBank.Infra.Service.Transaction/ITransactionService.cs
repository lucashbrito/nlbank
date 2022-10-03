using NLBank.Domain;

namespace NLBank.Infra.Service.Transaction
{
    public interface ITransactionService
    {
        Task CreateAsync(Guid operationUid, string iban, Operation operation, string details);
    }
}