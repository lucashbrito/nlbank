using Microsoft.AspNetCore.Mvc;
using NLBank.Api.Controllers.Customer.v1.Model;
using NLBank.Api.Filters;
using NLBank.Infra.Services.CreateCustomer;

namespace NLBank.Api.Controllers.Customer.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v1/customer")]
    [TypeFilter(typeof(ApiExceptionFilter))]
    public class CustomerController : ControllerBase
    {
        private ICreateCustomerFacade createCustomerFacade;
        public CustomerController(ICreateCustomerFacade createCustomerFacade)
        {
            this.createCustomerFacade = createCustomerFacade;
        }

        [HttpPost("", Name = "CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerModelV1 createCustomerModelV1)
        {
            (var customer, var account) = await createCustomerFacade.CreateCustomerUsingLibrary(createCustomerModelV1.FirsName, createCustomerModelV1.LastName);

            return Ok(new CreateCustomerResponseV1(customer, account));
        }
    }
}