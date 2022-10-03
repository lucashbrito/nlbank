using NLBank.Domain;
using NLBank.Domain.DomainObjects;
using NLBank.Infra.Service.Account;
using NLBank.Infra.Service.Transaction;

namespace NLBank.Infra.Services.TransferMoney
{
    public class TransferMoneyFacade : ITransferMoneyFacade
    {
        private IAccountService accountService;
        private ITransactionService transactionService;

        public TransferMoneyFacade(IAccountService accountService, ITransactionService transactionService)
        {
            this.accountService = accountService;
            this.transactionService = transactionService;
        }


        public async Task TransferMoney(string ibanSent, string ibanReceived, decimal money)
        {
            var accountSent = await accountService.GetByIbanAsync(ibanSent);

            if (accountSent == null)
                throw new DomainException("Account not found");

            var accountReceived = await accountService.GetByIbanAsync(ibanReceived);

            if (accountReceived == null)
                throw new DomainException("Account not found");

            accountSent.TransferMoney(accountReceived, money);

            await accountService.UpdateAsync(accountSent);

            var trackUid = Guid.NewGuid();

            await transactionService.CreateAsync(trackUid, accountSent.IBAN, Operation.TransferMoney, $"created: {DateTime.Now} any other info");

            await accountService.UpdateAsync(accountReceived);

            await transactionService.CreateAsync(trackUid, accountReceived.IBAN, Operation.ReceivedMoney, $"created: {DateTime.Now} any other info");
        }
    }
}
