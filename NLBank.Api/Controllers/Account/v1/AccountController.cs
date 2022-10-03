using Microsoft.AspNetCore.Mvc;
using NLBank.Api.Controllers.Account.v1.Model;
using NLBank.Api.Filters;
using NLBank.Infra.Services.PutMoney;
using NLBank.Infra.Services.TransferMoney;

namespace NLBank.Api.Controllers.Account.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v1/account")]
    [TypeFilter(typeof(ApiExceptionFilter))]
    public class AccountController : ControllerBase
    {
        private IPutMoneyFacade putMoneyFacade;
        private ITransferMoneyFacade transferMoneyFacade;

        public AccountController(IPutMoneyFacade putMoneyFacade, ITransferMoneyFacade transferMoneyFacade)
        {
            this.putMoneyFacade = putMoneyFacade;
            this.transferMoneyFacade = transferMoneyFacade;
        }

        [HttpPost("putmoney", Name = "PutMoney")]
        public async Task<IActionResult> PutMoney([FromBody] PutMoneyModelV1 putMoneyModelV1)
        {
            await putMoneyFacade.PutMoney(putMoneyModelV1.IBAN, putMoneyModelV1.Money);

            return Ok();
        }

        [HttpPost("transferMoney", Name = "Transfer")]
        public async Task<IActionResult> TransferMoney([FromBody] TransferMoneyModelV1 transferMoneyModelV1)
        {
            await transferMoneyFacade.TransferMoney(transferMoneyModelV1.IbanSent, transferMoneyModelV1.IbanReceived, transferMoneyModelV1.Money);

            return Ok();
        }
    }
}