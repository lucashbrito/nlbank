namespace NLBank.Infra.Services.TransferMoney
{
    public interface ITransferMoneyFacade
    {
        Task TransferMoney(string ibanSent, string ibanReceived, decimal money);
    }
}
