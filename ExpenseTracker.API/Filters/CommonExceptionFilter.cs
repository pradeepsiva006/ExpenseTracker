using ExpenseTracker.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ExpenseTracker.API.Filters
{
    public class CommonExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            LogException(context.Exception);

            var response = new BaseResponseDto
            {
                Success = false,
                Message = context.Exception.Message
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void LogException(Exception exception)
        {
            Console.WriteLine($"Exception logged: {exception.Message}");
        }
    }
}
