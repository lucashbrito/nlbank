using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLBank.Domain.DomainObjects;

namespace NLBank.Api.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public ApiExceptionFilter()
        {
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            HandleException(context);

            return Task.CompletedTask;
        }

        public void HandleException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                var ordersDomainException = new DomainException("Sorry, an unexpected error has occurred!");

                context.Result = new ObjectResult(ordersDomainException)
                {
                    StatusCode = 501
                };
            }
        }
    }
}
