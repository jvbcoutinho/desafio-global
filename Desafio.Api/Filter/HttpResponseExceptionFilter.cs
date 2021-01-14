using Desafio.Shared.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Desafio.API.Filter
{
    public class HttpResponseExceptionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is BusinessException bex)
            {
                var responseException = new HttpResponseException(bex.Message);

                context.Result = new ObjectResult(responseException)
                {
                    StatusCode = (int)System.Net.HttpStatusCode.UnprocessableEntity
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is AuthenticationException aex)
            {
                var responseException = new HttpResponseException(aex.Message);

                context.Result = new ObjectResult(responseException)
                {
                    StatusCode = (int)System.Net.HttpStatusCode.Unauthorized
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is NotFoundException nex)
            {
                var responseException = new HttpResponseException(nex.Message);

                context.Result = new ObjectResult(responseException)
                {
                    StatusCode = (int)System.Net.HttpStatusCode.NotFound
                };
                context.ExceptionHandled = true;
            }

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}