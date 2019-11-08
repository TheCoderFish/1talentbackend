using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneTalentResignation.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly   ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next,ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception exception)
                {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            _logger.LogCritical("Message: " + exception.Message + "Exception Details: " + exception.ToString());

            if (exception is HttpRequestException)
            {
                code = HttpStatusCode.BadRequest;
            }

            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(code.ToString());
        }
    }
}
