namespace NLBank.Api.Controllers.Account.v1.Model
{
    public class TransferMoneyModelV1
    {
        public string IbanSent { get; set; }
        public string IbanReceived { get; set; }
        public decimal Money { get; set; }
    }
}
