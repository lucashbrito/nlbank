namespace NLBank.Api.Controllers.Customer.v1.Model
{
    public class CreateCustomerResponseV1
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IBAN { get; set; }
        public decimal Money { get; set; }

        public CreateCustomerResponseV1(Domain.Customer customer, Domain.Account account)
        {
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            IBAN = customer.IBAN;
            Money = account.Money;
        }
    }
}
