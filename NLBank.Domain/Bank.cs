using NLBank.Domain.DomainObjects;

namespace NLBank.Domain
{
    public class Bank : Entity
    {
        public string BankCode { get; protected set; }
        public string Country { get; protected set; }
        public decimal Money { get; protected set; }

        protected Bank() { }

        public Bank(string bankCode, decimal money, string country)
        {
            BankCode = bankCode;
            Money = money;
            Country = country;
        }

        public decimal Fee(decimal CustomerMoney)
        {
            if (CustomerMoney < 0)
                throw new DomainException("money cannot be 0 or negative");

            var fee = CustomerMoney * 0.01m;

            Money += fee;

            return CustomerMoney - fee;
        }
    }
}
