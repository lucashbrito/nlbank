using NLBank.Domain;
using NLBank.Infra.Service.Account;
using NLBank.Infra.Service.Bank;
using NLBank.Infra.Service.Transaction;

namespace NLBank.Infra.Services.PutMoney
{
    public class PutMoneyFacade : IPutMoneyFacade
    {
        private IAccountService accountService;
        private ITransactionService transactionService;
        private IBankService bankService;

        public PutMoneyFacade(
            IAccountService accountService, ITransactionService transactionService, IBankService bankService)
        {
            this.accountService = accountService;
            this.transactionService = transactionService;
            this.bankService = bankService;
        }

        public async Task PutMoney(string iban, decimal money)
        {
            var account = await accountService.GetByIbanAsync(iban);

            var bankCode = account.GetBankCode();

            var bank = await bankService.GetByIbanAsync(bankCode);

            account.PutMoney(bank.Fee(money));

            await bankService.UpdateAsync(bank);

            var trackUid = Guid.NewGuid();

            await transactionService.CreateAsync(trackUid, account.IBAN, Operation.PutMoneyBank, $"created: {DateTime.Now} any other info");

            await accountService.UpdateAsync(account);

            await transactionService.CreateAsync(trackUid, account.IBAN, Operation.PutMoneyCustomerAccount, $"created: {DateTime.Now} any other info");
        }
    }
}
