namespace NLBank.Infra.Services.PutMoney
{
    public interface IPutMoneyFacade
    {
        Task PutMoney(string iban, decimal money);
    }
}
