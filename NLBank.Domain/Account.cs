using NLBank.Domain.DomainObjects;

namespace NLBank.Domain
{
    public class Account : Entity
    {
        public string IBAN { get; protected set; }
        public Guid CustomerId { get; protected set; }
        public decimal Money { get; protected set; }

        public Account(string iban, decimal money, Guid customerId)
        {
            if (string.IsNullOrWhiteSpace(iban))
                throw new DomainException("Iban cannot be null or empty");

            IBAN = iban;
            Money = money;
            CustomerId = customerId;
        }
        protected Account() { }

        public void TransferMoney(Account accountReceived, decimal money)
        {
            HasBalance(money);

            Money -= money;

            accountReceived.Money = accountReceived.Money += money;
        }

        public string GetBankCode()
        {
            return IBAN.Substring(4, 4);
        }

        public void PutMoney(decimal moneyToBePut)
        {
            Money += moneyToBePut;
        }

        private void HasBalance(decimal money)
        {
            if (money > Money)
                throw new DomainException("Account sent does not have enought money");
        }
    }
}
