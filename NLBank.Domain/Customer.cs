using NLBank.Domain.DomainObjects;

namespace NLBank.Domain
{
    public class Customer : Entity
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string IBAN { get; protected set; }

        protected Customer() { }

        public Customer(string firstName, string lastName, string iban)
        {
            FirstName = firstName;
            LastName = lastName;

            if (string.IsNullOrEmpty(iban))
                throw new DomainException("Iban cannot be null or empty");

            IBAN = iban;
        }
    }
}