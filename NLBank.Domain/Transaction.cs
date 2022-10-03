using NLBank.Domain.DomainObjects;

namespace NLBank.Domain
{
    public class Transaction : Entity
    {
        public Guid OperationId { get; protected set; }

        public string IBAN { get; protected set; }
        public Operation Operation { get; protected set; }
        public string Details { get; protected set; }

        protected Transaction() { }

        public Transaction(Guid operationid, string iban, Operation operation, string details)
        {
            if (string.IsNullOrEmpty(iban))
                throw new DomainException("Iban cannot be null or empty");

            OperationId = operationid;
            IBAN = iban;
            Operation = operation;
            Details = details;
        }

    }
}
